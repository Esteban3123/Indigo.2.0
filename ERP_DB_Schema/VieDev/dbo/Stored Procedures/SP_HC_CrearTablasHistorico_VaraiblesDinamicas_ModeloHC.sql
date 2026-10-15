
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