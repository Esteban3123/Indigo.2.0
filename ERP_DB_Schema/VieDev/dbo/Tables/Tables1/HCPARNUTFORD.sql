CREATE TABLE [dbo].[HCPARNUTFORD] (
    [ID]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCPARNUTCID]         INT             NULL,
    [CODE]                CHAR (3)        NOT NULL,
    [CODNAME]             CHAR (3)        NULL,
    [NAME]                CHAR (50)       NULL,
    [STATUS]              BIT             NOT NULL,
    [OBSERVATION]         VARCHAR (MAX)   NULL,
    [FORMULATE]           VARCHAR (5000)  NULL,
    [MinValuesCentral]    NUMERIC (18, 2) NULL,
    [MaxValuesCentral]    NUMERIC (18, 2) NULL,
    [MinValuesPeripheral] NUMERIC (18, 2) NULL,
    [MaxValuesPeripheral] NUMERIC (18, 2) NULL,
    [CentralAlert]        VARCHAR (500)   NULL,
    [PeripheralAlert]     VARCHAR (500)   NULL,
    CONSTRAINT [PK__HCPARNUT__3214EC27116BAB9F] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IDNUTCFORMULATE] FOREIGN KEY ([HCPARNUTCID]) REFERENCES [dbo].[HCPARNUTC] ([ID])
);


GO
ALTER TABLE [dbo].[HCPARNUTFORD] NOCHECK CONSTRAINT [FK_IDNUTCFORMULATE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje de alerta de la vía periférica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'PeripheralAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje de alerta de la vía central', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CentralAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Peripheral máximo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MaxValuesPeripheral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Peripheral mínimo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MinValuesPeripheral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor central máximo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MaxValuesCentral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor central minimo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MinValuesCentral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'FORMULATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'OBSERVATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del nombre de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CODNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'HCPARNUTCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'ID';

