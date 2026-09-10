-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 09/08/2016
-- Description:	Procedimiento Almacenado para Listar las Incapacidades en un Rango de Fechas
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ReportNovelties] @InitialDate          DATE, -- Fecha Inicial
                                               @EndDate              DATE, -- Fecha Final
                                               @InitialCodeGroup     VARCHAR(20), 
                                               @EndCodeGroup         VARCHAR(20), 
                                               @pBranchOfficeIdStart INT, 
                                               @pBranchOfficeIdEnd   INT
AS
    BEGIN
        DECLARE @IdNovelty INT;
        DECLARE @RealDateNovelty DATE;
        DECLARE @EndDateNovelty DATE;
        DECLARE @DaysNovelty INT;
        DECLARE @ValueNovelty NUMERIC(18, 0);
        DECLARE @EmployeeBaseSalary NUMERIC(18, 0);
        DECLARE @EPSRecognizeValue NUMERIC(18, 0);
        DECLARE @PaidPayrollValue NUMERIC(18, 0);
        DECLARE @TypeNovelty INT;
        IF @InitialCodeGroup IS NULL
           OR @InitialCodeGroup = ''
            BEGIN
                SELECT @InitialCodeGroup = '000000';
        END;
        IF @EndCodeGroup IS NULL
           OR @EndCodeGroup = ''
            BEGIN
                SET @EndCodeGroup = '999999';
        END;
        DECLARE @TablaNovelty TABLE
        (IdNovelty           INT, 
         EmployeeBaseSalary  NUMERIC(18, 0), 
         EPSRecognizeValue   NUMERIC(18, 0), 
         PaidPayrollValue    NUMERIC(18, 0), 
         GroupCode           VARCHAR(20), 
         GroupName           VARCHAR(100), 
         EmployeeId          INT, 
         NitEmployee         VARCHAR(20), 
         NameEmployee        VARCHAR(100), 
         EmployeeBasicSalary NUMERIC(18, 0), 
         TypeNovelty         INT, 
         NameTypeNovelty     VARCHAR(20), 
         NoveltyClass        VARCHAR(30), 
         SubNoveltyClass     VARCHAR(30), 
         RealDate            DATE, 
         EndDate             DATE, 
         DaysNovelty         INT, 
         Reason              VARCHAR(200), 
         Value               NUMERIC(18, 0), 
         ResolutionNumber    VARCHAR(30), 
         ResolutionDate      DATE, 
         NoveltyRegistryDate DATETIME, 
         BranchOfficeId      INT
        );
        INSERT INTO @TablaNovelty
        (IdNovelty, 
         EmployeeBaseSalary, 
         EPSRecognizeValue, 
         PaidPayrollValue, 
         GroupCode, 
         GroupName, 
         EmployeeId, 
         NitEmployee, 
         NameEmployee, 
         EmployeeBasicSalary, 
         TypeNovelty, 
         NameTypeNovelty, 
         NoveltyClass, 
         SubNoveltyClass, 
         RealDate, 
         EndDate, 
         DaysNovelty, 
         Reason, 
         Value, 
         ResolutionNumber, 
         ResolutionDate, 
         NoveltyRegistryDate, 
         BranchOfficeId
        )
               SELECT N.Id, 
                      N.EmployeeBaseSalary, 
                      N.EPSRecognizeValue, 
                      N.PaidPayrollValue, 
                      G.Code, 
                      G.Name, 
                      E.Id, 
                      TP.Nit, 
                      TP.Name, 
                      N.Value, 
                      N.TypeNovelty,
                      CASE
                          WHEN N.TypeNovelty = 1
                          THEN 'INCAPACIDAD'
                          WHEN N.TypeNovelty = 2
                          THEN 'SANCIÓN'
                          ELSE 'LICENCIA'
                      END, 
                      '0', 
                      '0', 
                      N.RealDate, 
                      N.EndDate, 
                      N.[Days], 
                      UPPER(N.Reason), 
                      N.Value, 
                      N.ResolutionNumber, 
                      N.ResolutionDate, 
                      N.CreationDate, 
                      fUnit.BranchOfficeId
               FROM Payroll.Novelty N
                    INNER JOIN Payroll.Employee E
							ON N.EmployeeId = E.Id
					INNER JOIN Common.ThirdParty TP
							ON E.ThirdPartyId = TP.Id
                    INNER JOIN Payroll.[Group] G
							ON G.Id = N.GroupId
					INNER JOIN 
					( 
						SELECT MAX(Id) Id, EmployeeId
						FROM Payroll.[Contract]
						GROUP BY EmployeeId
					) cntrc ON E.Id = cntrc.EmployeeId 
					INNER JOIN Payroll.Contract C ON C.Id = cntrc.Id
                    INNER JOIN Payroll.FunctionalUnit fUnit
					ON C.FunctionalUnitId = fUnit.Id
              WHERE  N.RealDate >= @InitialDate AND
                     N.RealDate <= @EndDate
                     AND G.Code BETWEEN @InitialCodeGroup AND @EndCodeGroup
                     AND (fUnit.BranchOfficeId BETWEEN @pBranchOfficeIdStart AND @pBranchOfficeIdEnd
                          OR (@pBranchOfficeIdEnd IS NULL
                              AND @pBranchOfficeIdStart IS NULL));
        DECLARE C_Novelty CURSOR
        FOR

            -- NOVEDADES
            SELECT IdNovelty, 
                   RealDate, 
                   EndDate, 
                   DaysNovelty, 
                   Value, 
                   EmployeeBaseSalary, 
                   EPSRecognizeValue, 
                   PaidPayrollValue, 
                   TypeNovelty
            FROM @TablaNovelty;
        OPEN C_Novelty;
        FETCH NEXT FROM C_Novelty INTO @IdNovelty, @RealDateNovelty, @EndDateNovelty, @DaysNovelty, @ValueNovelty, @EmployeeBaseSalary, @EPSRecognizeValue, @PaidPayrollValue, @TypeNovelty;
        WHILE @@FETCH_STATUS = 0
            BEGIN

                -- Averiguo los Días de la Incapacidad

                DECLARE @Days INT= @DaysNovelty;
                DECLARE @TmpValueNovelty NUMERIC(18, 0)= @ValueNovelty;
                DECLARE @NoveltyClass VARCHAR(30)= '';
                DECLARE @NoveltySubClass VARCHAR(30)= '';
                IF @EndDateNovelty > @EndDate
                    BEGIN
                        -- Averiguo cuántos dias de Incapacidad tiene el periodo
                        IF @RealDateNovelty >= @InitialDate
                            BEGIN
                                -- Si la Novedad inició en el Periodo
                                SET @Days = DATEDIFF(DAY, @RealDateNovelty, @EndDate) + 1;
                        END;
                            ELSE
                            BEGIN
                                -- Si la Novedad viene de un mes anterior
                                SET @Days = DATEDIFF(DAY, @InitialDate, @EndDate) + 1;
                        END;
                        SET @TmpValueNovelty = (@ValueNovelty / 30) * @Days;
                END;
                IF @RealDateNovelty < @InitialDate
                    BEGIN
                        -- Para Novedades que iniciaron el mes anterior

                        IF @EndDateNovelty < @EndDate
                            BEGIN
                                -- La novedad inició el mes anterior y finaliza en este
                                SET @Days = DATEDIFF(DAY, @InitialDate, @EndDateNovelty) + 1;
                        END;
                            ELSE
                            BEGIN
                                -- La Novedad inició el mes anterior y termina el próximo mes
                                SET @Days = DATEDIFF(DAY, @InitialDate, @EndDate) + 1;
                        END;
                        SET @TmpValueNovelty = (@ValueNovelty / 30) * @Days;
                END;
                IF @TypeNovelty = 1
                    BEGIN
                        -- INCAPACIDAD
                        DECLARE @TmpInabilityClass INT;
                        DECLARE @TmpRiskType INT;
                        SELECT @TmpInabilityClass = InabilityClass, 
                               @TmpRiskType = RiskType
                        FROM Payroll.Novelty
                        WHERE Id = @IdNovelty;
                        IF @TmpInabilityClass = 1
                            BEGIN
                                SET @NoveltyClass = 'AMBULATORIA';
                        END;
                            ELSE
                            IF @TmpInabilityClass = 2
                                BEGIN
                                    SET @NoveltyClass = 'HOSPITALARIA';
                            END;
                                ELSE
                                IF @TmpInabilityClass = 3
                                    BEGIN
                                        SET @NoveltyClass = 'MATERNIDAD';
                                END;
                                    ELSE
                                    IF @TmpInabilityClass = 4
                                        BEGIN
                                            SET @NoveltyClass = 'ENFERMEDAD PROFESIONAL';
                                    END;
                                        ELSE
                                        IF @TmpInabilityClass = 5
                                            BEGIN
                                                SET @NoveltyClass = 'LICENCIA DE PATERNIDAD';
                                        END;
                                            ELSE
                                            IF @TmpInabilityClass = 6
                                                BEGIN
                                                    SET @NoveltyClass = 'ENFERMEDAD GENERAL';
                                            END;
                        IF @TmpInabilityClass = 4
                            BEGIN
                                DECLARE @SubNoveltyClass VARCHAR(30);
                                IF @TmpRiskType = 1
                                    BEGIN
                                        SET @NoveltySubClass = 'ACCIDENTE DE TRABAJO';
                                END;
                                    ELSE
                                    IF @TmpRiskType = 2
                                        BEGIN
                                            SET @NoveltySubClass = 'ENFERMEDAD OCUPACIONAL';
                                    END;
                        END;
                END;
                    ELSE
                    IF @TypeNovelty = 3
                        BEGIN
                            -- LICENCIAS
                            DECLARE @LicensesClass INT;
                            SELECT @LicensesClass = LicenseClass
                            FROM Payroll.Novelty
                            WHERE Id = @IdNovelty;
                            IF @LicensesClass = 1
                                BEGIN
                                    SET @NoveltyClass = 'REMUNERADA';
                            END;
                                ELSE
                                IF @LicensesClass = 2
                                    BEGIN
                                        SET @NoveltyClass = 'NO REMUNERADA';
                                END;
                                    ELSE
                                    IF @LicensesClass = 3
                                        BEGIN
                                            SET @NoveltyClass = 'PERMISO';
                                    END;
                                        ELSE
                                        IF @LicensesClass = 4
                                            BEGIN
                                                SET @NoveltyClass = 'CON CARGO A VACACIONES';
                                        END;
                                            ELSE
                                            IF @LicensesClass = 5
                                                BEGIN
                                                    SET @NoveltyClass = 'CALAMIDAD DOMESTICA';
                                            END;
                                                ELSE
                                                IF @LicensesClass = 6
                                                    BEGIN
                                                        SET @NoveltyClass = 'LICENCIA DE LUTO';
                                                END;
                    END;
                UPDATE @TablaNovelty
                  SET 
                      DaysNovelty = @Days, 
                      Value = @TmpValueNovelty, 
                      NoveltyClass = @NoveltyClass, 
                      SubNoveltyClass = @NoveltySubClass
                WHERE IdNovelty = @IdNovelty;
                FETCH NEXT FROM C_Novelty INTO @IdNovelty, @RealDateNovelty, @EndDateNovelty, @DaysNovelty, @ValueNovelty, @EmployeeBaseSalary, @EPSRecognizeValue, @PaidPayrollValue, @TypeNovelty;
            END; -- fin while de C_Novelty
        CLOSE C_Novelty;
        DEALLOCATE C_Novelty;
        SELECT *
        FROM @TablaNovelty;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de novedades de nómina (incapacidades, sanciones y licencias) para un rango de fechas y grupo de empleados. Consulta las novedades registradas en Payroll.Novelty cruzando con el empleado, su tercero asociado (NIT y nombre), el grupo de nómina y la unidad funcional del último contrato vigente. Clasifica cada novedad por tipo (incapacidad, sanción o licencia), calcula el tramo de días correspondiente y los valores reconocidos por la EPS versus los pagados por la empresa, permitiendo filtrar por fecha de inicio de la novedad, rango de código de grupo y sede. Se utiliza para generar informes de control y auditoría de ausentismo y novedades que afectan la liquidación de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ReportNovelties';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ReportNovelties';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de novedades de nómina (incapacidades, sanciones y licencias) en un rango de fechas, prorrateando días y valores cuando la novedad cruza los límites del período y describiendo en texto las clases y subclases.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango @InitialDate/@EndDate debe estar definido para filtrar Payroll.Novelty.RealDate.; Si @InitialCodeGroup viene NULL o vacío, se asume ''000000''; si @EndCodeGroup viene NULL o vacío, se asume ''999999''.; El filtro de sucursal aplica solo si ambos @pBranchOfficeIdStart y @pBranchOfficeIdEnd no son NULL; si ambos son NULL, no se filtra por sucursal.; Cada empleado debe tener al menos un Payroll.Contract (se toma el de mayor Id) con FunctionalUnit asociada.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el contrato más reciente por empleado (MAX(Contract.Id) por EmployeeId).; El prorrateo de valor usa base mensual fija de 30 días: Value = (ValorOriginal/30)*Días.; Cuando la novedad excede el período, los días reportados nunca superan los días contenidos dentro del rango [InitialDate, EndDate].; El reporte solo incluye novedades cuyo RealDate cae dentro del rango solicitado (no incluye por EndDate).; El código de grupo siempre se evalúa con un rango por defecto ''000000''-''999999'' si no se especifican límites.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Incapacidad; Sanción; Licencia; Maternidad; Paternidad; Enfermedad general; Enfermedad profesional; Accidente de trabajo; Enfermedad ocupacional; Calamidad doméstica; Licencia de luto; Reconocimiento EPS; Salario base de liquidación; Grupo de nómina; Unidad funcional; Sucursal; Contrato laboral', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TablaNovelty: Inserta novedades cuyo RealDate está entre @InitialDate y @EndDate, con código de grupo entre @InitialCodeGroup y @EndCodeGroup, vinculadas al último contrato (MAX(Id)) de cada empleado y opcionalmente filtradas por rango de BranchOfficeId.; [UPDATE] @TablaNovelty: Para cada novedad recorrida por cursor, actualiza DaysNovelty, Value (prorrateado), NoveltyClass y SubNoveltyClass según el tipo y clase de incapacidad/licencia.; [RETURN_RESULT] (resultset): Retorna todas las filas de @TablaNovelty al finalizar el procesamiento del cursor.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si N.TypeNovelty = 1 / 2 / otro → Etiqueta NameTypeNovelty como ''INCAPACIDAD'' / ''SANCIÓN'' / ''LICENCIA'' respectivamente.; si @EndDateNovelty > @EndDate → Recalcula días: si @RealDateNovelty >= @InitialDate usa DATEDIFF(@RealDateNovelty,@EndDate)+1; si no, usa DATEDIFF(@InitialDate,@EndDate)+1. Recalcula Value = (@ValueNovelty/30)*@Days.; si @RealDateNovelty < @InitialDate → Novedad iniciada en período anterior: si @EndDateNovelty < @EndDate usa DATEDIFF(@InitialDate,@EndDateNovelty)+1, si no DATEDIFF(@InitialDate,@EndDate)+1. Recalcula Value = (@ValueNovelty/30)*@Days.; si @TypeNovelty = 1 (INCAPACIDAD) → Mapea InabilityClass: 1=AMBULATORIA, 2=HOSPITALARIA, 3=MATERNIDAD, 4=ENFERMEDAD PROFESIONAL, 5=LICENCIA DE PATERNIDAD, 6=ENFERMEDAD GENERAL.; si @TypeNovelty = 1 AND InabilityClass = 4 → Mapea RiskType: 1=ACCIDENTE DE TRABAJO, 2=ENFERMEDAD OCUPACIONAL en SubNoveltyClass.; si @TypeNovelty = 3 (LICENCIA) → Mapea LicenseClass: 1=REMUNERADA, 2=NO REMUNERADA, 3=PERMISO, 4=CON CARGO A VACACIONES, 5=CALAMIDAD DOMESTICA, 6=LICENCIA DE LUTO.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Novelty; Payroll.Employee; Common.ThirdParty; Payroll.Group; Payroll.Contract; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportNovelties';
-- GO
