
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_EsquemasInformacionCAC]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

Select 
Rtrim(Schemes.Description) +'  -  Ciclo: ' + RTRIM(Ciclos.CICLO) + '/' + Rtrim(ORD.CICLOS) AS 'NOMBRE ESQUEMA',
CASE ORD.[48] 
WHEN 1 THEN '1=Neoadyuvancia (manejo  inicial prequirúrgico)' 
WHEN 2 THEN '2=Tratamiento inicial curativo sin cirugía sugerida' 
WHEN 3 THEN '3=Adyuvancia(manejo inicial postquirúrgico)' 
WHEN 11 THEN '11=Manejo de recaída' 
WHEN 12 THEN '12=Manejo de enfermedad metastásica' 
WHEN 13 THEN '13=Manejo paliativo (sin manejo de recaída ni enfermedad metastásica' 
END AS 'UBICACION',
CASE ORD.FASEQUIMIOTERAPIA
WHEN 1 THEN 'Prefase o citorreducción inicial' 
WHEN 2 THEN 'Inducción' 
WHEN 3 THEN 'Intensificación' 
WHEN 4 THEN 'Consolidación' 
WHEN 5 THEN 'Reinducción' 
WHEN 6 THEN 'Mantenimiento' 
WHEN 7 THEN 'Mantenimiento largo o final' 
WHEN 8 THEN 'Otra Fase' 
END AS 'FASE',
ORD.[46] AS 'CANTIDAD', Ciclos.NUMINGRES, Ciclos.NUMEFOLIO
FROM [EHR].[HCORDCICLOS] Ciclos With(Nolock)																								 
INNER JOIN [EHR].Schemes Schemes With(Nolock) ON Schemes.Id = Ciclos.SchemesId
INNER JOIN [EHR].HCORDQUIMIO ORD With(Nolock) ON ORD.Id = Ciclos.IDHCORDQUIMIO
WHERE Ciclos.IPCODPACI = @CodigoPaciente AND Ciclos.NUMINGRES= @NumeroIngreso AND Ciclos.NUMEFOLIO = @NumeroFolio
      
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los esquemas de quimioterapia aplicados a un paciente específico, dado su código (cédula), número de ingreso y número de folio. Combina los ciclos de tratamiento oncológico (HCORDCICLOS), el catálogo de esquemas terapéuticos (Schemes) y las órdenes de quimioterapia (HCORDQUIMIO) para mostrar el nombre del esquema, el número de ciclo actual sobre el total, la ubicación o intención del tratamiento (neoadyuvancia, adyuvancia, paliativo, metastásico, etc.), la fase del ciclo de quimioterapia (inducción, consolidación, mantenimiento, etc.) y la cantidad asociada. Se utiliza en la historia clínica del Centro de Atención de Cáncer (CAC) para visualizar el resumen del protocolo oncológico activo del paciente en un ingreso o folio determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el esquema de quimioterapia, ciclo, ubicación clínica del tratamiento, fase y cantidad asociados a una orden oncológica de un paciente para un ingreso y folio específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registros en HCORDCICLOS asociados al ingreso y folio indicados.; El ciclo debe estar relacionado con un esquema vigente en EHR.Schemes y con una orden de quimioterapia en EHR.HCORDQUIMIO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven ciclos que tengan correspondencia obligatoria con un esquema (Schemes) y con una orden de quimioterapia (HCORDQUIMIO) por INNER JOIN.; El nombre del esquema siempre se presenta con el formato ''Descripción - Ciclo: actual/total''.; Los códigos de ubicación distintos a {1,2,3,11,12,13} y de fase distintos a {1..8} resultan en NULL en sus columnas etiquetadas.; La consulta opera en modo solo lectura con NOLOCK en todas las tablas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Esquema de quimioterapia; Ciclo de tratamiento; Fase de quimioterapia; Neoadyuvancia; Adyuvancia; Manejo de recaída; Enfermedad metastásica; Manejo paliativo; Ingreso hospitalario; Folio clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] EHR.HCORDCICLOS: Devuelve nombre de esquema con ciclo actual/total, ubicación, fase, cantidad, ingreso y folio, filtrando por paciente, ingreso y folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ORD.[48] = 1 → Etiqueta UBICACION como Neoadyuvancia (manejo inicial prequirúrgico); si ORD.[48] = 2 → Etiqueta UBICACION como Tratamiento inicial curativo sin cirugía sugerida; si ORD.[48] = 3 → Etiqueta UBICACION como Adyuvancia (manejo inicial postquirúrgico); si ORD.[48] = 11 → Etiqueta UBICACION como Manejo de recaída; si ORD.[48] = 12 → Etiqueta UBICACION como Manejo de enfermedad metastásica; si ORD.[48] = 13 → Etiqueta UBICACION como Manejo paliativo; si ORD.FASEQUIMIOTERAPIA entre 1 y 8 → Traduce el código numérico a la fase clínica correspondiente (Prefase, Inducción, Intensificación, Consolidación, Reinducción, Mantenimiento, Mantenimiento largo o final, Otra Fase)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDCICLOS; EHR.Schemes; EHR.HCORDQUIMIO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasInformacionCAC';
-- GO
