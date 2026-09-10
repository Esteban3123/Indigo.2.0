

CREATE PROCEDURE [dbo].[SPREP_HC_Generales_InterpretacionParaclinicos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio Char(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
     
SELECT  IDETIPHIS, RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',RTRIM(INTERPRET) AS INTERPRETACION,NUMEFOLIO AS 'NUMERO DE FOLIO',RTRIM(NUMEFOLIO)+'-'+RTRIM(A.CODSERIPS) AS 'NUMERO DE FOLIO LLAVE',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI, CASE CORRELACION When 1 Then 'Si' When 2 Then 'NO' When 3 Then 'Sin especificar' END AS CORRELACION, RTRIM(OBSERVACIONCORRELA) AS OBSERVACIONCORRELA
FROM HCORDIMAG A With(Nolock) 
INNER JOIN INCUPSIPS B With(Nolock) ON A.CODSERIPS=B.CODSERIPS 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMFOLINT=@NumeroFolio
UNION SELECT IDETIPHIS, RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',RTRIM(INTERPRET) AS INTERPRETACION,NUMEFOLIO AS 'NUMERO DE FOLIO',RTRIM(NUMEFOLIO)+'-'+RTRIM(A.CODSERIPS) AS 'NUMERO DE FOLIO LLAVE',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI, CASE CORRELACION When 1 Then 'Si' When 2 Then 'NO' When 3 Then 'Sin especificar' END AS CORRELACION, RTRIM(OBSERVACIONCORRELA) AS OBSERVACIONCORRELA
FROM HCORDLABO A  With(Nolock)
INNER JOIN INCUPSIPS B With(Nolock) ON A.CODSERIPS=B.CODSERIPS 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMFOLINT=@NumeroFolio
UNION SELECT IDETIPHIS, RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',RTRIM(INTERPRET) AS INTERPRETACION,NUMEFOLIO AS 'NUMERO DE FOLIO',RTRIM(NUMEFOLIO)+'-'+RTRIM(A.CODSERIPS) AS 'NUMERO DE FOLIO LLAVE',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI, CASE CORRELACION When 1 Then 'Si' When 2 Then 'NO' When 3 Then 'Sin especificar' END AS CORRELACION, RTRIM(OBSERVACIONCORRELA) AS OBSERVACIONCORRELA
FROM HCORDPATO A With(Nolock)
INNER JOIN INCUPSIPS B With(Nolock) ON A.CODSERIPS=B.CODSERIPS
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMFOLINT=@NumeroFolio
                  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene la interpretación de todos los paraclínicos (exámenes de laboratorio, imágenes diagnósticas y patología) registrados en la historia clínica de un paciente para un ingreso y folio específicos. Consolida en un único resultado los estudios de laboratorio (HCORDLABO), imágenes diagnósticas como radiografías, ecografías y tomografías (HCORDIMAG) y estudios de patología (HCORDPATO), enriqueciendo cada uno con la descripción del servicio CUPS desde el maestro de servicios (INCUPSIPS). Para cada estudio devuelve el tipo de historia, la descripción del examen, la interpretación clínica registrada, el número de folio, el centro de atención, la unidad funcional, la cédula del paciente y la correlación clínica con su observación. Se utiliza para visualizar en la historia clínica el resultado e interpretación de todos los paraclínicos solicitados durante un ingreso o atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único resultado las interpretaciones de paraclínicos (imágenes, laboratorio y patología) ordenados a un paciente en un ingreso y folio específicos, con la descripción del servicio y la correlación clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso y folio deben existir y estar relacionados en las órdenes de imágenes, laboratorio o patología.; Los códigos de servicio registrados en las órdenes deben existir en el catálogo de CUPS/IPS para obtener la descripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La correlación clínica solo se traduce a texto para los valores 1, 2 y 3; cualquier otro valor queda como NULL.; Cada fila se identifica con una llave compuesta por número de folio + código de servicio.; Las consultas se realizan con NOLOCK, asumiendo lecturas sucias aceptables.; Solo se incluyen registros que tengan correspondencia en el catálogo de servicios (INNER JOIN con INCUPSIPS).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de historia clínica; Órdenes de imágenes diagnósticas; Órdenes de laboratorio clínico; Órdenes de patología; Interpretación de paraclínicos; Correlación clínica; Catálogo CUPS/IPS; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la unión (UNION elimina duplicados) de las interpretaciones de órdenes de imágenes diagnósticas, laboratorio clínico y patología filtradas por paciente, ingreso y folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CORRELACION = 1 → Se reporta ''Si'' (existe correlación clínica).; si CORRELACION = 2 → Se reporta ''NO'' (no existe correlación clínica).; si CORRELACION = 3 → Se reporta ''Sin especificar''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.HCORDLABO; dbo.HCORDPATO; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_InterpretacionParaclinicos';
-- GO
