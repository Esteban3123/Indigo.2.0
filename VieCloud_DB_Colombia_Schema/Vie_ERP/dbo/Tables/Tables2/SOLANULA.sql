CREATE TABLE [dbo].[SOLANULA] (
    [AUTO]      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COMAUTON]  INT           NOT NULL,
    [FECHANUL]  SMALLDATETIME NOT NULL,
    [MOTIVOS]   VARCHAR (800) NOT NULL,
    [CODUSUARI] CHAR (20)     NOT NULL,
    CONSTRAINT [PK_SOLANULA] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_SOLANULA_SOLCOMPRA] FOREIGN KEY ([COMAUTON]) REFERENCES [dbo].[SOLCOMPRA] ([COMAUTON])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario/profesional de la salud que realiza la anulación de la solicitud de compra. Identificador único del operador que cancela/revoca.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del usuario que realiza la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivos, razones o justificación de la anulación de la solicitud de compra. Texto descriptivo que explica por qué se cancela/revoca la compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'MOTIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'motivos de la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'MOTIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'MOTIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la anulación de la solicitud de compra. Registro temporal (SMALLDATETIME) del momento en que se efectúa la cancelación/revocación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'FECHANUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'FECHANUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'FECHANUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico de la solicitud de compra que se anula. Referencia a la solicitud (FK a SOLCOMPRA) que está siendo cancelada/revocada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'COMAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'autonumerico de la solicitud que se anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'COMAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'COMAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico identificador único de la anulación registrada. Clave primaria de la tabla de anulaciones de solicitudes de compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de anulaciones de solicitudes: guarda cada vez que una solicitud es anulada, con la fecha, el motivo de la anulación y el usuario que la realizó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA';
