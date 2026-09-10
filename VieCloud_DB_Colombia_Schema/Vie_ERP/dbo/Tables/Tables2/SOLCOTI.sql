CREATE TABLE [dbo].[SOLCOTI] (
    [AUTO]       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODUSUARI]  CHAR (20)     NOT NULL,
    [FECHCOTI]   DATETIME      NOT NULL,
    [FECHREGIS]  DATE          NOT NULL,
    [VIGENCOTI]  DATE          NOT NULL,
    [PLAZOCOTI]  INT           NOT NULL,
    [FORMACOTI]  TINYINT       NOT NULL,
    [OBSERCOTI]  VARCHAR (500) NULL,
    [PROVECOTI]  NUMERIC (18)  NOT NULL,
    [ESTADCOTI]  CHAR (1)      NULL,
    [NOMARCHIVO] VARCHAR (MAX) NULL,
    [CODCENATE]  CHAR (10)     NULL,
    CONSTRAINT [PK_SOLCOTI_1] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_SOLCOTI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_SOLCOTI_SOLCOTI] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[SOLCOTI] ([AUTO]),
    CONSTRAINT [FK_SOLCOTI_SOLPROVEE] FOREIGN KEY ([PROVECOTI]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO]),
    CONSTRAINT [FK_SOLCOTI_SOLTIPAGO] FOREIGN KEY ([FORMACOTI]) REFERENCES [dbo].[SOLTIPAGO] ([Autonumerico])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (unidad funcional, clínica, sede, punto de atención). CHAR(10). FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo adjunto a la cotización (documento, soporte, anexo). VARCHAR(MAX). Nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'NOMARCHIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el nombre de de los archivos adjuntos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'NOMARCHIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'NOMARCHIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cotización (pendiente, aprobada, rechazada, vencida). CHAR(1). Nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'ESTADCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el estado de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'ESTADCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'ESTADCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) del proveedor de la cotización. NUMERIC(18). FK a SOLPROVEE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'PROVECOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'PROVECOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'PROVECOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas sobre la cotización (comentarios, aclaraciones, condiciones especiales). VARCHAR(500). Nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'OBSERCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene las observaciones de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'OBSERCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'OBSERCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o forma de pago de la cotización (contado, crédito, plazo). TINYINT. FK a SOLTIPAGO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FORMACOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la el autonumerico de la forma de pago de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FORMACOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FORMACOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo de entrega de la cotización en días (tiempo de espera, días de entrega). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'PLAZOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el plazo de entrega de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'PLAZOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'PLAZOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vigencia de la cotización (fecha de vencimiento, validez hasta). DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'VIGENCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la vigencia de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'VIGENCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'VIGENCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro o creación de la cotización en el sistema. DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la fecha que se registra la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la cotización (emisión, solicitud). DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FECHCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la fecha de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FECHCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FECHCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que realiza o registra la cotización. CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo de usuario que realiza la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) de la solicitud de cotización. INT IDENTITY. PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de cotización realizadas a proveedores. Registra las cotizaciones pedidas, su vigencia, forma de pago, estado y el centro de atención que las genera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI';
