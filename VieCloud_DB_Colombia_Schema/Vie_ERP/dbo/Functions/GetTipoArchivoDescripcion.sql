
CREATE FUNCTION dbo.GetTipoArchivoDescripcion
(
    @TipoArchivo VARCHAR(2) -- Recibe el código (0,1,2,...)
)
RETURNS NVARCHAR(300)
AS
BEGIN
    DECLARE @Descripcion NVARCHAR(300);

    SET @Descripcion = CASE @TipoArchivo
        WHEN '0'  THEN 'Administrativo'
        WHEN '1'  THEN 'Asistencial'
        WHEN '2'  THEN 'Comprobante de recibido del usuario'
        WHEN '3'  THEN 'Descripción quirúrgica'
        WHEN '4'  THEN 'Epicrisis'
        WHEN '5'  THEN 'Evidencia del envió del tramite respectivo'
        WHEN '6'  THEN 'Factura de venta del material de osteosíntesis expedida por el proveedor'
        WHEN '7'  THEN 'Factura de venta en salud'
        WHEN '8'  THEN 'Factura de venta por el cobro a la aseguradora SOAT, la ADRES o la entidad que haga sus veces'
        WHEN '9'  THEN 'Hoja de administración de medicamentos'
        WHEN '10' THEN 'Hoja de atención de urgencia'
        WHEN '11' THEN 'Hoja de atención odontológica'
        WHEN '12' THEN 'Lista de precios'
        WHEN '13' THEN 'Orden o prescripción facultativa'
        WHEN '14' THEN 'Registro de anestesia'
        WHEN '15' THEN 'Registro individual de prestación de servicios - RIPS'
        WHEN '16' THEN 'Resumen de atención u hoja de evolución'
        WHEN '17' THEN 'Resultado de los procedimientos de apoyo diagnóstico'
        WHEN '18' THEN 'Transporte no asistencial ambulatorio de la persona'
        WHEN '19' THEN 'Traslado asistencial de pacientes'
        WHEN '20' THEN 'Otro'
        ELSE 'No definido'
    END

    RETURN @Descripcion;
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de archivo (soporte clínico/administrativo) a su descripción textual normativa para uso en facturación y soportes en salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetTipoArchivoDescripcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de tipo de archivo se recibe como cadena de hasta 2 caracteres.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetTipoArchivoDescripcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna una cadena no nula (en el peor caso ''No definido'').; El catálogo de tipos de archivo está fijo en código y abarca códigos ''0'' a ''20'' más una opción ''Otro'' (20).; Las descripciones reflejan los soportes documentales exigidos en el sector salud (RIPS, epicrisis, factura SOAT/ADRES, hoja quirúrgica, etc.).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetTipoArchivoDescripcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Soportes de facturación en salud; RIPS (Registro Individual de Prestación de Servicios); Epicrisis; Descripción quirúrgica; Hoja de administración de medicamentos; Atención de urgencias; Atención odontológica; Orden o prescripción facultativa; Registro de anestesia; Factura de venta en salud; SOAT; ADRES; Material de osteosíntesis; Transporte/traslado asistencial; Apoyo diagnóstico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetTipoArchivoDescripcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando el código coincide con un valor entre ''0'' y ''20'', devuelve la descripción correspondiente al tipo de soporte (p.ej. ''0''→Administrativo, ''7''→Factura de venta en salud, ''15''→RIPS).; [RETURN_RESULT] N/A: Cuando el código no corresponde a ninguno de los valores definidos (''0''-''20''), devuelve ''No definido''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetTipoArchivoDescripcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoArchivo IN (''0''..''20'') → Retorna la descripción específica del tipo de soporte clínico/administrativo asociada al código. else Retorna la cadena ''No definido''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetTipoArchivoDescripcion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetTipoArchivoDescripcion';
GO
