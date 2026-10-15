CREATE TABLE [dbo].[HCORHEMEA] (
    [ID]           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMBOLID] INT            NOT NULL,
    [HCEVENADVID]  INT            NOT NULL,
    [OBSERVACION]  VARCHAR (2000) NULL,
    [FECREGISTRO]  DATETIME       NOT NULL,
    [USUREGISTRO]  CHAR (20)      NOT NULL,
    CONSTRAINT [PK_HCORHEMEA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMEA_HCORHEMBOL] FOREIGN KEY ([HCORHEMBOLID]) REFERENCES [dbo].[HCORHEMBOL] ([ID])
);


GO
ALTER TABLE [dbo].[HCORHEMEA] NOCHECK CONSTRAINT [FK_HCORHEMEA_HCORHEMBOL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMEA', @level2type = N'COLUMN', @level2name = N'USUREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Feha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMEA', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMEA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del evento adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMEA', @level2type = N'COLUMN', @level2name = N'HCEVENADVID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de bolsa de sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMEA', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMEA', @level2type = N'COLUMN', @level2name = N'ID';

