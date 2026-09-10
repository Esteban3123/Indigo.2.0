CREATE TABLE [dbo].[HCCATDOCU] (
    [CODCATEGO] CHAR (3)      NOT NULL,
    [DESCATEGO] VARCHAR (100) NOT NULL,
    [TIPDOCUME] VARCHAR (2)   NULL,
    [ESTCATDOC] BIT           NULL,
    CONSTRAINT [PK_HCCATDOCU] PRIMARY KEY CLUSTERED ([CODCATEGO] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de categoría de documento (activo/inactivo). Bit booleano que indica si la categoría está habilitada para uso en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'ESTCATDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'ESTCATDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'ESTCATDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento clasificado según normativa RIPS y operativa sanitaria: 0=Administrativo, 1=Asistencial, 2=Comprobante recibido usuario, 3=Descripción quirúrgica, 4=Epicrisis, 5=Evidencia trámite, 6=Factura osteosíntesis proveedor, 7=Factura venta salud, 8=Factura SOAT/ADRES, 9=Hoja medicamentos, 10=Urgencia, 11=Odontología, 12=Lista precios, 13=Orden/Prescripción, 14=Anestesia, 15=RIPS, 16=Evolución, 17=Apoyo diagnóstico, 18=Transporte ambulatorio, 19=Traslado asistencial, 20=Otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'TIPDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Documento 0:Administrativo    1:Asistencial  2:Comprobante de recibido del usuario  3:Descripción quirúrgica  4:Epicrisis  5:Evidencia del envío del trámite respectivo  6:Factura de venta del material de osteosíntesis expedida por el proveedor  7:Factura de venta en salud  8:Factura de venta por el cobro a la aseguradora SOAT, la ADRES o la entidad que haga sus veces  9:Hoja de administración de medicamentos  10:Hoja de atención de urgencia  11:Hoja de atención odontológica  12:Lista de precios  13:Orden o prescripción facultativa 14:Registro de anestesia  15:Registro individual de prestación de servicios - RIPS  16:Resumen de atención u hoja de evolución  17:Resultado de los procedimientos de apoyo diagnóstico  18:Transporte no asistencial ambulatorio de la persona  19:Traslado asistencial de pacientes  20:Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'TIPDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'TIPDOCUME';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la categoría del documento. Identifica la clasificación para organizar registros clínicos, administrativos y facturación en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'DESCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la categoria del documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'DESCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'DESCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (3 caracteres) que identifica unívocamente la categoría de documento. Clave primaria para búsqueda y referencia en documentos del paciente, atención y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la categoria del documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'CODCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de categorías de documentos clínicos o administrativos. Define los tipos de agrupaciones de documentos usados en la historia clínica, indicando su código, descripción, tipo de documento asociado y si la categoría está activa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU';
