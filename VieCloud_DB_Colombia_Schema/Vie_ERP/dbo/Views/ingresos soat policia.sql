
CREATE VIEW [dbo].[ingresos soat policia]
AS
SELECT        dbo.ADINGRESO.IINGREPOR AS Expr1, dbo.ADINGRESO.ITIPORIES AS Expr3, dbo.ADINGRESO.ICAUSAING AS Expr4, dbo.INPACIENT.CODENTIDA AS Expr2, 
                         dbo.ADINGRESO.NUMINGRES, dbo.ADINGRESO.IPCODPACI, dbo.ADINGRESO.IFECHAING
FROM            dbo.ADINGRESO INNER JOIN
                         dbo.INPACIENT ON dbo.ADINGRESO.IPCODPACI = dbo.INPACIENT.IPCODPACI
WHERE        (dbo.ADINGRESO.IINGREPOR = 1) AND (dbo.ADINGRESO.ICAUSAING = 6) AND (dbo.INPACIENT.CODENTIDA = '00130')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta que filtra los ingresos hospitalarios atendidos bajo el amparo del SOAT (Seguro Obligatorio de Accidentes de Tránsito) correspondientes a la Policía Nacional, identificada con el código de entidad `00130`. Se restringe a ingresos con tipo de ingreso `1` y causa de ingreso `6`, cruzando admisiones con el maestro de pacientes para obtener el código de entidad aseguradora. Sirve como apoyo para reporting o facturación de eventos de tránsito vinculados a dicha aseguradora.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ingresos soat policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ingresos soat policia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los ingresos de pacientes adscritos a la entidad Policía (''00130'') cuya admisión corresponde a la causa SOAT, filtrando por tipo y causa de ingreso específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ingresos soat policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en ADINGRESO con su paciente correspondiente en INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ingresos soat policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone ingresos cuyo tipo de ingreso (IINGREPOR) es 1; Solo expone ingresos cuya causa de ingreso (ICAUSAING) es 6 (asociada a SOAT); Solo expone pacientes asociados a la entidad ''00130'' (Policía); Requiere correspondencia entre paciente del ingreso y maestro de pacientes (INNER JOIN por IPCODPACI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ingresos soat policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión de paciente; Paciente; Entidad responsable de pago; Causa de ingreso SOAT; Convenio Policía Nacional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ingresos soat policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Cuando IINGREPOR=1 AND ICAUSAING=6 AND CODENTIDA=''00130'', se retornan los datos del ingreso y su paciente asociado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ingresos soat policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ingresos soat policia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ingresos soat policia';
GO
