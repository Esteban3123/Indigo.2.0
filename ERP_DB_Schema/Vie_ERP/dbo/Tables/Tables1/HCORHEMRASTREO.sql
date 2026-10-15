CREATE TABLE [dbo].[HCORHEMRASTREO] (
    [ID]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMCOID]        INT           NOT NULL,
    [CODIGOBACTERIOLOGO] CHAR (20)     NOT NULL,
    [FECHARASTREO]       DATETIME      NOT NULL,
    [FECHAREGISTRO]      DATETIME      NOT NULL,
    [CODIGOPARAMETRO]    VARCHAR (20)  NULL,
    [NOMBREPARAMETRO]    VARCHAR (100) NULL,
    [RESULTADO]          VARCHAR (100) NULL,
    [VALORREFERENCIA]    VARCHAR (100) NULL,
    [NOMBREBACTERIOLOGO] VARCHAR (150) NULL,
    CONSTRAINT [PK_HCORHEMRASTREO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMRASTREO_HCORHEMCO] FOREIGN KEY ([HCORHEMCOID]) REFERENCES [dbo].[HCORHEMCO] ([ID]),
    CONSTRAINT [FK_HCORHEMRASTREO_INPROFSAL] FOREIGN KEY ([CODIGOBACTERIOLOGO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCORHEMRASTREO] NOCHECK CONSTRAINT [FK_HCORHEMRASTREO_HCORHEMCO];


GO
ALTER TABLE [dbo].[HCORHEMRASTREO] NOCHECK CONSTRAINT [FK_HCORHEMRASTREO_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre del bacteriologo del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'NOMBREBACTERIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el valor de referencia del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'VALORREFERENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el resultado del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre del parametro del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'NOMBREPARAMETRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el código del parametro del rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'CODIGOPARAMETRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de realizacion del rastreo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'FECHARASTREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo profesional que hace el rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'CODIGOBACTERIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Orden  (HCRODHEMCO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMRASTREO', @level2type = N'COLUMN', @level2name = N'ID';

