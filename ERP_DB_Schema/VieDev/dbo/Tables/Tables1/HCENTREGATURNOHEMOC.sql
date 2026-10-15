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
ALTER TABLE [dbo].[HCENTREGATURNOHEMOC] NOCHECK CONSTRAINT [FK_HCENTREGATURNOHEMOC_HCCOMSAN];


GO
ALTER TABLE [dbo].[HCENTREGATURNOHEMOC] NOCHECK CONSTRAINT [FK_HCENTREGATURNOHEMOC_HCENTREGATURNOHEMOC];


GO
ALTER TABLE [dbo].[HCENTREGATURNOHEMOC] NOCHECK CONSTRAINT [FK_HCENTREGATURNOHEMOC_HCORHEMCO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del componente sanguineo  1: Solicitud Reserva  2: Solicitud de Transfusión  3. Reserva sin Solicitud de Transfusión  4. Reserva con Solicitud de Transfusión  5. Liberado  6. Entregado  7. Aplicado  8. No Aplicado  9. Anulado  10. Descartado por salida de paciente  11. Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del componente sanguíneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'COMSAMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que relaciona el ID de la tabla HCORHEMCO (Cabecera solictud de hemocomponentes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla HCORHEMBOL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera HCENTREGATURNOCPACIENTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera HCENTREGATURNO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOHEMOC', @level2type = N'COLUMN', @level2name = N'ID';

