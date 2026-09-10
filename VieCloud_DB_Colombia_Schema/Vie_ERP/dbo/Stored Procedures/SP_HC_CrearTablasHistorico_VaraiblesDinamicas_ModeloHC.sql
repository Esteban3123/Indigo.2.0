
-- =============================================  
-- Author:  Rafael Eduardo Patiño Cabrera  
-- ALTER date: 09/05/2019  
-- Description: crea tabla historico para el informe de - odontologia - por x periodo de tiempo  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC] @FechaInicial AS DATE, 
                                                                               @FechaFinal AS   DATE, 
                                                                               @IDMODELOHC AS   INT
AS
    BEGIN
        DECLARE @table AS TABLE
        (ID        INT IDENTITY(1, 1), 
         IDHiSPACA INT
        );
        INSERT INTO @table
               SELECT B.ID AS IdHispaca
               FROM dbo.ODONTOCONTROL A
                    INNER JOIN dbo.HCHISPACA B WITH(NOLOCK) ON A.NUMEFOLIO = B.NUMEFOLIO
                                                               AND A.IPCODPACI = B.IPCODPACI
                                                               AND A.NUMINGRES = B.NUMINGRES
                    INNER JOIN dbo.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
                    INNER JOIN dbo.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
                    INNER JOIN Contract.CareGroup E WITH(NOLOCK) ON E.Id = D.GENCAREGROUP
                    INNER JOIN dbo.INENTIDAD F WITH(NOLOCK) ON F.CODENTIDA = D.CODENTIDA
                    INNER JOIN dbo.INPROFSAL G WITH(NOLOCK) ON G.CODPROSAL = A.CODPROSAL
                    INNER JOIN dbo.ODONTOCONTROLVALO H WITH(NOLOCK) ON H.IDODONTOCONTROL = A.ID
                    INNER JOIN dbo.INDIAGNOS I WITH(NOLOCK) ON I.CODDIAGNO = B.CODDIAGNO
                    LEFT JOIN dbo.HCRIESGOSP J WITH(NOLOCK) ON J.NUMINGRCES = A.NUMINGRES
               WHERE B.FECHISPAC BETWEEN @FechaInicial AND @FechaFinal
                     AND B.IDMODELOHC = @IDMODELOHC;
        BEGIN TRAN SubirVariablesHistorico;
        BEGIN TRY
            DECLARE @count AS INT=
            (
                SELECT COUNT(ID)
                FROM @table
            );
            DECLARE @contador AS INT= 1;
            DECLARE @SQL NVARCHAR(MAX)= N'';
            DECLARE @SQLCrearTabla NVARCHAR(MAX)= N'';

            --odontologia  
            IF @IDMODELOHC = 28
                BEGIN
                    IF EXISTS
                    (
                        SELECT *
                        FROM sysobjects
                        WHERE xtype = 'u'
                              AND name = 'ESE_HC_HISTORICO_ODONTOLOGIA'
                    )
                        BEGIN
                            DROP TABLE ESE_HC_HISTORICO_ODONTOLOGIA;
                    END;
                    WHILE @contador <= @count
                        BEGIN
                            DECLARE @IdtmpHC AS INT=
                            (
                                SELECT IDHiSPACA
                                FROM @table
                                WHERE ID = @contador
                            );
                            SET @SQL =
                            (
                                SELECT Query
                                FROM dbo.fnListarVariablesDinamicas2(@IdtmpHC)
                            );
                            IF @contador = 1
                                BEGIN
                                    SET @SQLCrearTabla = 'SELECT * INTO ESE_HC_HISTORICO_ODONTOLOGIA FROM ( ' + @SQL + ' ) as x';
                                    EXEC sp_executesql 
                                         @SQLCrearTabla;
                            END;
                                ELSE
                                BEGIN
                                    INSERT INTO ESE_HC_HISTORICO_ODONTOLOGIA
                                    EXEC sp_executesql 
                                         @SQL;
                            END;
                            SET @contador = @contador + 1;
                            PRINT @contador;
            END;
                    COMMIT TRAN SubirVariablesHistorico;
                    SELECT *
                    FROM ESE_HC_HISTORICO_ODONTOLOGIA;
            END;

            --Higiene oral  
            IF @IDMODELOHC = 33
                BEGIN
                    IF EXISTS
                    (
                        SELECT *
                        FROM sysobjects
                        WHERE xtype = 'u'
                              AND name = 'ESE_HC_HISTORICO_HIGIENEORAL'
                    )
                        BEGIN
                            DROP TABLE ESE_HC_HISTORICO_HIGIENEORAL;
                    END;
                    WHILE @contador <= @count
                        BEGIN
                            SET @IdtmpHC =
                            (
                                SELECT IDHiSPACA
                                FROM @table
                                WHERE ID = @contador
                            );
                            SET @SQL =
                            (
                                SELECT Query
                                FROM dbo.fnListarVariablesDinamicas2(@IdtmpHC)
                            );
                            IF @contador = 1
                                BEGIN
                                    SET @SQLCrearTabla = 'SELECT * INTO ESE_HC_HISTORICO_HIGIENEORAL FROM ( ' + @SQL + ' ) as x';
                                    EXEC sp_executesql 
                                         @SQLCrearTabla;
                            END;
                                ELSE
                                BEGIN
                                    INSERT INTO ESE_HC_HISTORICO_HIGIENEORAL
                                    EXEC sp_executesql 
                                         @SQL;
                            END;
                            SET @contador = @contador + 1;
                            PRINT @contador;
            END;
                    COMMIT TRAN SubirVariablesHistorico;
                    SELECT *
                    FROM ESE_HC_HISTORICO_HIGIENEORAL;
            END;
        END TRY
        BEGIN CATCH
            ROLLBACK TRAN SubirVariablesHistorico;
        END CATCH;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera tablas históricas temporales con las variables dinámicas de historias clínicas odontológicas para un rango de fechas y un modelo de historia clínica (HC) específico. Cruza controles odontológicos (ODONTOCONTROL) con sus historias clínicas (HCHISPACA), datos del paciente (INPACIENT), ingreso (ADINGRESO), entidad pagadora (INENTIDAD), profesional tratante (INPROFSAL), índices CEO/CPO (ODONTOCONTROLVALO) y diagnósticos CIE-10 (INDIAGNOS) para reunir todos los registros del período. Según el modelo HC indicado, crea dinámicamente una tabla física de resultado: ESE_HC_HISTORICO_ODONTOLOGIA (modelo 28 – odontología general), ESE_HC_HISTORICO_HIGIENEORAL (modelo 33 – higiene oral) u otras variantes, poblándola registro a registro mediante SQL dinámico generado por la función fnListarVariablesDinamicas2. Se usa para producir informes históricos de atención odontológica por período, integrando todas las variables clínicas y administrativas del episodio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye tablas históricas dinámicas (odontología o higiene oral) consolidando las variables clínicas de las historias dentro de un rango de fechas y un modelo de HC, recreándolas en cada ejecución.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El @IDMODELOHC debe ser 28 (odontología) o 33 (higiene oral); cualquier otro valor no produce salida.; Debe existir la función dbo.fnListarVariablesDinamicas2 que retorne un SQL ejecutable por cada historia clínica.; Las historias deben tener FECHISPAC dentro del rango y coincidir con el modelo HC indicado.; Los folios deben tener correspondencia entre ODONTOCONTROL y HCHISPACA por NUMEFOLIO, IPCODPACI y NUMINGRES.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla histórica destino se reconstruye desde cero en cada ejecución (DROP + SELECT INTO en la primera fila).; La estructura de columnas de la tabla histórica queda determinada por la primera historia procesada (SELECT * INTO inicial).; Solo se procesan historias cuya FECHISPAC esté dentro del rango y cuyo modelo de HC coincida con el parámetro.; Si ocurre cualquier error durante la carga, se revierte toda la operación vía ROLLBACK.; El procedimiento está acotado a los modelos HC 28 y 33; otros modelos no generan efectos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica odontológica; Higiene oral; Modelo de historia clínica; Variables dinámicas de HC; Ingreso/atención del paciente; Diagnóstico; Profesional de la salud; Entidad pagadora; Grupo de atención (CareGroup); Riesgos en salud pública', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] dbo.ESE_HC_HISTORICO_ODONTOLOGIA: Si @IDMODELOHC=28 y la tabla existe en sysobjects, se ejecuta DROP TABLE antes de reconstruirla.; [INSERT] dbo.ESE_HC_HISTORICO_ODONTOLOGIA: Si @IDMODELOHC=28: en la primera iteración crea la tabla con SELECT * INTO a partir del SQL dinámico; en iteraciones siguientes hace INSERT INTO con el resultado de sp_executesql sobre el SQL dinámico de cada historia.; [DELETE] dbo.ESE_HC_HISTORICO_HIGIENEORAL: Si @IDMODELOHC=33 y la tabla existe en sysobjects, se ejecuta DROP TABLE antes de reconstruirla.; [INSERT] dbo.ESE_HC_HISTORICO_HIGIENEORAL: Si @IDMODELOHC=33: en la primera iteración crea la tabla con SELECT * INTO desde el SQL dinámico; en las siguientes hace INSERT INTO con el resultado de sp_executesql.; [RETURN_RESULT] dbo.ESE_HC_HISTORICO_ODONTOLOGIA: Tras consolidar (modelo 28), retorna SELECT * de la tabla histórica de odontología.; [RETURN_RESULT] dbo.ESE_HC_HISTORICO_HIGIENEORAL: Tras consolidar (modelo 33), retorna SELECT * de la tabla histórica de higiene oral.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @IDMODELOHC = 28 → Procesa flujo de odontología: DROP/CREATE/INSERT sobre ESE_HC_HISTORICO_ODONTOLOGIA y devuelve sus filas.; si @IDMODELOHC = 33 → Procesa flujo de higiene oral: DROP/CREATE/INSERT sobre ESE_HC_HISTORICO_HIGIENEORAL y devuelve sus filas.; si La tabla histórica destino ya existe en sysobjects (xtype=''u'') → Se elimina con DROP TABLE antes de reconstruirla. else Se omite el DROP y se procede directamente a crearla en la primera iteración.; si @contador = 1 dentro del WHILE → Crea la tabla histórica usando SELECT * INTO con el SQL dinámico de la primera historia. else Realiza INSERT INTO sobre la tabla ya creada con el SQL dinámico de cada historia siguiente.; si Se produce una excepción dentro del TRY → Se ejecuta ROLLBACK de la transacción SubirVariablesHistorico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.fnListarVariablesDinamicas2; sp_executesql', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ODONTOCONTROL; dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; Contract.CareGroup; dbo.INENTIDAD; dbo.INPROFSAL; dbo.ODONTOCONTROLVALO; dbo.INDIAGNOS; dbo.HCRIESGOSP; sys.sysobjects', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_CrearTablasHistorico_VaraiblesDinamicas_ModeloHC';
-- GO
