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