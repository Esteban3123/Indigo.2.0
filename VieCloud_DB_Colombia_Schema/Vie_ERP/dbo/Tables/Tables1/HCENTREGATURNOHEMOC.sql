CREATE TABLE [dbo].[HCENTREGATURNOHEMOC] (
    [ID]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCENTREGATURNOC]       INT NOT NULL,
    [IDHCENTREGATURNOCPACIEN] INT NOT NULL,
    [IDBOLSA]                 INT NOT NULL,
    [HCORHEMCOID]             INT NOT NULL,
    [COMSAMID]                INT NOT NULL,
    [ESTADO]                  INT NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNOHEMOCOMPONENTES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCENTREGATURNOHEMOC_HCCOMSAN] FOREIGN KEY ([COMSAMID]) REFERENCES [dbo].[HCCOMSAN] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOHEMOC_HCENTREGATURNOC] FOREIGN KEY ([IDHCENTREGATURNOC]) REFERENCES [dbo].[HCENTREGATURNOC] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOHEMOC_HCENTREGATURNOCPACIEN] FOREIGN KEY ([IDHCENTREGATURNOCPACIEN]) REFERENCES [dbo].[HCENTREGATURNOCPACIEN] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOHEMOC_HCENTREGATURNOHEMOC] FOREIGN KEY ([IDBOLSA]) REFERENCES [dbo].[HCORHEMBOL] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOHEMOC_HCORHEMCO] FOREIGN KEY ([HCORHEMCOID]) REFERENCES [dbo].[HCORHEMCO] ([ID])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del componente sanguíneo (tipo INT): 1=Solicitud Reserva, 2=Solicitud Transfusión, 3=Reserva sin Solicitud, 4=Reserva con Solicitud, 5=Liberado, 6=Entregado, 7=Aplicado, 8=No Aplicado, 9=Anulado, 10=Descartado por salida paciente, 11=Extramural. Ciclo de vida de hemoterapia/transfusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del componente sanguineo  1: Solicitud Reserva  2: Solicitud de Transfusión  3. Reserva sin Solicitud de Transfusión  4. Reserva con Solicitud de Transfusión  5. Liberado  6. Entregado  7. Aplicado  8. No Aplicado  9. Anulado  10. Descartado por salida de paciente  11. Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del componente sanguíneo (FK → HCCOMSAN.ID). Referencia a tipo de hemocomponente: glóbulos rojos, plaquetas, plasma, crioprecipitado, etc. PII sensible en transfusionología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'COMSAMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del componente sanguíneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'COMSAMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'COMSAMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/solicitud de hemocomponentes (FK → HCORHEMCO.ID). Agrupa solicitud médica de transfusión para el paciente en el turno de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que relaciona el ID de la tabla HCORHEMCO (Cabecera solictud de hemocomponentes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de bolsa física de hemocomponente (FK → HCORHEMBOL.ID). Referencia al registro individual de sangre/plasma del banco de sangre o hemoteca.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla HCORHEMBOL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paciente en el turno de atención (FK → HCENTREGATURNOCPACIEN.ID). Vínculo del paciente específico a quien se entrega/aplica el componente sanguíneo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera HCENTREGATURNOCPACIENTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera del turno de atención/ingreso (FK → HCENTREGATURNOC.ID). Contexto de atención clínica donde se tramita la solicitud y entrega de hemoterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera HCENTREGATURNO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario autoincremental (INT IDENTITY). Clave única del detalle de entrega de hemocomponente en el turno del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de entrega de turnos de hemocomponentes (sangre y derivados). Asocia cada bolsa de hemocomponente entregada a un turno de hemoterapia, vinculando el paciente, la orden de hemocomponente y el estado de la entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC';
