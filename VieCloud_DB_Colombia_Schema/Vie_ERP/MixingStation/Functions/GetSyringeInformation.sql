
CREATE FUNCTION [MixingStation].[GetSyringeInformation]
(
	@Origin varchar(100),
	@OriginId int
)

Returns @SyringeInformation Table
(
	CODVIAADM varchar(100),
	AdministrationName varchar(100)
) 

As
Begin	
			
	IF @Origin is not null  and @OriginId is not null
		begin
			If @Origin = 'HCPRESCRA'
				begin
				insert into @SyringeInformation
			    SELECT  h.CODVIAADM, ar.DESVIAADM as AdministrationName from HCPRESCRA h
				left join HCVIAADMI ar on ar.CODVIAADM = h.CODVIAADM
				where h.Id = @OriginId
				
			end
			else if @Origin ='HCINFLIQA'
				begin 
				insert into @SyringeInformation
				SELECT VIAADMDIL as CODVIAADM, ar.DESVIAADM as AdministrationName from HCINFLIQA h
				left join dbo.HCINFLIQD  hc on hc.CODCONCEC = H.CODCONCEC
				left join HCVIAADMI ar on ar.CODVIAADM = hc.VIAADMDIL 
				where h.CONSECUTI = @OriginId
			end	
			else if @Origin ='HCORDMEDICAM'
				begin 
				insert into @SyringeInformation
				SELECT h.CODVIAADM as CODVIAADM, ar.DESVIAADM as AdministrationName  from EHR.HCORDMEDICAM h
				left join HCVIAADMI ar on ar.CODVIAADM = h.CODVIAADM 
				where h.Id = @OriginId
			end	
	END
	ELSE
		BEGIN
		INSERT into @SyringeInformation
		VALUES(NULL,NULL)
	END
	Return
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que obtiene la información de vía de administración (código y nombre descriptivo) asociada a una jeringa o preparación farmacéutica, según el origen de la prescripción. Recibe dos parámetros: el tipo de origen (@Origin) y su identificador (@OriginId), y consulta tres fuentes distintas según el contexto: prescripciones generales (HCPRESCRA), infusiones de líquidos (HCINFLIQA/HCINFLIQD) y órdenes de medicamentos oncológicos/quimioterapia (HCORDMEDICAM), resolviendo en todos los casos el nombre legible de la vía de administración desde el catálogo HCVIAADMI. Es utilizada por la estación de mezclas farmacéuticas (MixingStation) para determinar cómo debe administrarse el medicamento preparado, ya sea por vía oral, intravenosa, intramuscular u otras, integrando información de historia clínica con el proceso de preparación y dispensación de jeringas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'GetSyringeInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'FUNCTION', @level1name = N'GetSyringeInformation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código y nombre de la vía de administración asociada al origen de una preparación (prescripción, infusión de líquidos u orden de medicamento de quimioterapia) para apoyar el módulo de mezclas / jeringas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetSyringeInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@Origin y @OriginId deben ser no nulos para ejecutar las consultas; en caso contrario se retorna fila (NULL,NULL); @Origin debe tomar uno de los valores reconocidos: ''HCPRESCRA'', ''HCINFLIQA'' o ''HCORDMEDICAM''; @OriginId debe corresponder a la clave esperada por cada tabla origen: Id en HCPRESCRA y EHR.HCORDMEDICAM, CONSECUTI en HCINFLIQA', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetSyringeInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre devuelve al menos una fila: si faltan parámetros, se devuelve (NULL,NULL); Si @Origin no coincide con ''HCPRESCRA'', ''HCINFLIQA'' ni ''HCORDMEDICAM'', la tabla resultante queda vacía; Para origen HCINFLIQA la vía retornada corresponde a la del diluyente (VIAADMDIL), no a la del medicamento principal; La descripción de la vía siempre proviene del catálogo HCVIAADMI mediante LEFT JOIN (puede ser NULL si no existe el código)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetSyringeInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'vía de administración; jeringa / preparación de mezcla; prescripción médica; infusión de líquidos; orden de medicamento (quimioterapia); diluyente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetSyringeInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @SyringeInformation: Cuando @Origin=''HCPRESCRA'', retorna CODVIAADM y DESVIAADM de HCPRESCRA join HCVIAADMI filtrando por h.Id=@OriginId; [RETURN_RESULT] @SyringeInformation: Cuando @Origin=''HCINFLIQA'', retorna VIAADMDIL del detalle HCINFLIQD (join por CODCONCEC) y DESVIAADM de HCVIAADMI filtrando por h.CONSECUTI=@OriginId; [RETURN_RESULT] @SyringeInformation: Cuando @Origin=''HCORDMEDICAM'', retorna CODVIAADM y DESVIAADM de EHR.HCORDMEDICAM join HCVIAADMI filtrando por h.Id=@OriginId; [RETURN_RESULT] @SyringeInformation: Cuando @Origin o @OriginId es NULL, inserta una fila con valores (NULL, NULL)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetSyringeInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Origin = ''HCPRESCRA'' → Obtiene CODVIAADM y descripción desde HCPRESCRA cruzada con HCVIAADMI usando Id = @OriginId; si @Origin = ''HCINFLIQA'' → Obtiene VIAADMDIL (vía de administración del diluyente) desde HCINFLIQA join HCINFLIQD por CODCONCEC, y descripción desde HCVIAADMI; filtra por CONSECUTI = @OriginId; si @Origin = ''HCORDMEDICAM'' → Obtiene CODVIAADM desde EHR.HCORDMEDICAM cruzada con HCVIAADMI usando Id = @OriginId; si @Origin IS NULL OR @OriginId IS NULL → Inserta una fila con (NULL, NULL) en el resultado', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetSyringeInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.HCVIAADMI; dbo.HCINFLIQA; dbo.HCINFLIQD; EHR.HCORDMEDICAM', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetSyringeInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'FUNCTION', @level1name=N'GetSyringeInformation';
GO
