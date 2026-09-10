CREATE FUNCTION [dbo].[CargarDosisXcicloXMedicamento]	
(
 @TIPO INT, --1-Dosis; 2-Insumos
 @IDHCORDQUIMIO INT,
 @CICLO   INT,
 @codigomedicamento varchar(100),
 @Dia as varchar 
 
 )
RETURNS varchar(1000)
AS
BEGIN
	  DECLARE @valores VARCHAR(1000)

IF  @TIPO = 1 --Medicamentos

	BEGIN  
		 SELECT @valores= COALESCE(@valores + ' / ', '') +  Rtrim(administracion)
			  FROM (
				 SELECT 'Dosis: ' + Rtrim(convert(numeric(18,2), m.DOSISPROD)) +' '+ Rtrim(LTRIM(F.ABRUNIMED)) + ' de forma '+ Rtrim(LTRIM(D.DESVIAADM)) + ' Día(s):' + /*Rtrim(H.DESCRIPCIONDIA)*/  + STUFF(( SELECT ', ' + CONVERT(VARCHAR(20),DIA) FROM ehr.HCORDMEDICAM WHERE IDHCORDQUIMIO = @IDHCORDQUIMIO  and CICLO = @CICLO  AND CODPRODUC = @codigomedicamento and DOSISPROD = m.DOSISPROD  FOR XML PATH('')),1,2, '')  as 'Administracion' 
				  from ehr.HCORDMEDICAM M  With(Nolock)
 						  INNER JOIN ehr.HCORMEDICAMESQUEMA H With(Nolock) on M.CODPRODUC = H.CODPRODUC AND M.CICLO = H.CICLO AND H.IDHCORDQUIMIO = @IDHCORDQUIMIO AND H.CICLO = @CICLO
						  INNER JOIN dbo.HCVIAADMI D With(Nolock) ON m.CODVIAADM = D.CODVIAADM																 
						  INNER JOIN dbo.INUNIMEDI F With(Nolock) ON m.CODUNIMED = F.CODUNIMED	
				 WHERE M.IDHCORDQUIMIO = @IDHCORDQUIMIO and M.CICLO = @CICLO AND M.CODPRODUC = @codigomedicamento/* AND ID = @IDHCORDMEDICAM*/ GROUP BY f.ABRUNIMED,d.DESVIAADM,M.DOSISPROD,H.DESCRIPCIONDIA
			 ) AS administracion

	END
ELSE IF @TIPO = 2 --Insumos
	BEGIN
	
			SELECT @valores =(SELECT ' Día(s):' + STUFF(( SELECT DISTINCT ', ' + CONVERT(VARCHAR(20),DIA) FROM ehr.HCORDINSUMOS With(Nolock) WHERE IDHCORDQUIMIO = @IDHCORDQUIMIO  and CICLO = @CICLO  AND CODIGOINSUMO = @codigomedicamento FOR XML PATH('')),1,2, '')  as 'Dias' )
				
	END

ELSE IF @TIPO = 4 --Confirmar Quimioterapia Enfermeria
	BEGIN
	
		SELECT @valores= COALESCE(@valores + ' / ', 'Administrar por una sola vez ' ) +  Rtrim(administracion)  
			  FROM (
				 SELECT case TypePrescription 
					 when 1 then  Rtrim(convert(numeric(18,2), m.DOSISPROD)) +' '+ Rtrim(LTRIM(F.ABRUNIMED)) + ' de forma '+ Rtrim(LTRIM(D.DESVIAADM))
					 when 2 then  Rtrim(convert(numeric(18,2), m.DOSISPROD)) +' '+ Rtrim(LTRIM(F.ABRUNIMED)) + ' a las '+  format(m.HORAFRECUDIA,'HH:mm')
				   end as 'Administracion' 
				from ehr.HCORDMEDICAM M  With(Nolock)
 						  INNER JOIN ehr.HCORMEDICAMESQUEMA H With(Nolock) on M.CODPRODUC = H.CODPRODUC AND M.CICLO = H.CICLO AND H.IDHCORDQUIMIO = @IDHCORDQUIMIO AND H.CICLO = @CICLO 
						  INNER JOIN dbo.HCVIAADMI D With(Nolock) ON m.CODVIAADM = D.CODVIAADM																 
						  INNER JOIN dbo.INUNIMEDI F With(Nolock) ON m.CODUNIMED = F.CODUNIMED	
				 WHERE M.IDHCORDQUIMIO = @IDHCORDQUIMIO and M.CICLO = @CICLO AND M.DIA = @Dia AND M.CODPRODUC = @codigomedicamento 
				 GROUP BY f.ABRUNIMED,d.DESVIAADM,M.DOSISPROD,H.DESCRIPCIONDIA,m.HORAFRECUDIA ,TypePrescription
			 ) AS administracion 	
	END

ELSE IF @TIPO = 3 --Diluyentes

			SELECT @valores =(SELECT ' Día(s):' + STUFF(( SELECT DISTINCT ', ' + CONVERT(VARCHAR(20),DIA) FROM ehr.HCORDMEDICAM With(Nolock) WHERE IDHCORDQUIMIO = @IDHCORDQUIMIO  and CICLO = @CICLO  AND CODDILUYENTE = @codigomedicamento FOR XML PATH('')),1,2, '')  as 'Dias' )

 return @valores

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que construye y retorna un texto descriptivo con la información de dosificación de un medicamento, insumo o diluyente dentro de un ciclo específico de quimioterapia. Según el tipo solicitado (1=medicamentos, 2=insumos, 3=diluyentes, 4=confirmación de enfermería), consolida la dosis, unidad de medida, vía de administración y los días de administración correspondientes al ciclo oncológico indicado. Para medicamentos, combina datos de las órdenes de quimioterapia (HCORDMEDICAM), el esquema de protocolo oncológico (HCORMEDICAMESQUEMA), el catálogo de vías de administración (HCVIAADMI) y las unidades de medida (INUNIMEDI), devolviendo una cadena legible como ''Dosis: 200 mg de forma Intravenosa Día(s): 1, 3, 5''. Se utiliza para presentar en pantalla o en documentos clínicos el resumen de prescripción de un medicamento oncológico por ciclo y paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CargarDosisXcicloXMedicamento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CargarDosisXcicloXMedicamento';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye una cadena descriptiva de la administración de un medicamento, insumo o diluyente por ciclo de quimioterapia, agrupando días de aplicación y detalles de dosis/vía/unidad según el tipo solicitado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CargarDosisXcicloXMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la orden de quimioterapia (IDHCORDQUIMIO) con su ciclo en las tablas correspondientes; Para tipo 1 y 4, el medicamento debe existir tanto en HCORDMEDICAM como en HCORMEDICAMESQUEMA para el ciclo y orden indicados; Para tipo 4, debe especificarse el día de administración; Los códigos de vía de administración y unidad de medida deben existir en los catálogos HCVIAADMI e INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CargarDosisXcicloXMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los días siempre se concatenan eliminando el separador inicial '', '' usando STUFF; En tipo 2 y 3 los días se obtienen como DISTINCT; En tipo 4 el prefijo inicial siempre es ''Administrar por una sola vez ''; Múltiples bloques de administración para tipo 1 y 4 se separan con '' / ''; El valor numérico de dosis siempre se formatea como numeric(18,2); La hora se formatea siempre como ''HH:mm'' en TypePrescription=2', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CargarDosisXcicloXMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Quimioterapia; Ciclo de tratamiento; Dosis de medicamento; Vía de administración; Unidad de medida; Insumos médicos; Diluyentes; Esquema de medicamentos; Confirmación de enfermería; Tipo de prescripción (por vía u horaria); Días de administración', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CargarDosisXcicloXMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando @TIPO=1, retorna texto ''Dosis: <dosis> <unidad> de forma <vía> Día(s): <lista días>'' concatenando múltiples administraciones separadas por '' / ''; [RETURN_RESULT] N/A: Cuando @TIPO=2 (Insumos), retorna '' Día(s): <lista distinta de días>'' a partir de HCORDINSUMOS; [RETURN_RESULT] N/A: Cuando @TIPO=3 (Diluyentes), retorna '' Día(s): <lista distinta de días>'' filtrando HCORDMEDICAM por CODDILUYENTE; [RETURN_RESULT] N/A: Cuando @TIPO=4 (Confirmar Quimioterapia Enfermería), retorna prefijo ''Administrar por una sola vez '' seguido de la descripción según TypePrescription: si TypePrescription=1 incluye dosis+unidad+vía; si TypePrescription=2 incluye dosis+unidad+hora (HH:mm)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CargarDosisXcicloXMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TIPO = 1 → Construye descripción de medicamentos con dosis, unidad, vía y lista de días agrupados por dosis; si @TIPO = 2 → Construye lista de días distintos desde HCORDINSUMOS para el insumo indicado; si @TIPO = 3 → Construye lista de días distintos desde HCORDMEDICAM filtrando por CODDILUYENTE; si @TIPO = 4 → Construye descripción para confirmación de enfermería filtrando además por @Dia, diferenciando por TypePrescription (1=por vía, 2=por hora); si TypePrescription = 1 (dentro de @TIPO=4) → Formato ''dosis unidad de forma vía'' else Si TypePrescription = 2: formato ''dosis unidad a las HH:mm''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CargarDosisXcicloXMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ehr.HCORDMEDICAM; ehr.HCORMEDICAMESQUEMA; dbo.HCVIAADMI; dbo.INUNIMEDI; ehr.HCORDINSUMOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CargarDosisXcicloXMedicamento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CargarDosisXcicloXMedicamento';
GO
