CREATE TABLE [dbo].[HCORHEMSV] (
    [ID]           INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMBOLID] INT                                                                            NOT NULL,
    [TIPO]         TINYINT                                                                        NOT NULL,
    [TA]           VARCHAR (7) MASKED WITH (FUNCTION = 'partial(0, "BloodPressure_Ofuscado", 0)') NOT NULL,
    [FC]           INT MASKED WITH (FUNCTION = 'default()')                                       NOT NULL,
    [T]            NUMERIC (18, 1) MASKED WITH (FUNCTION = 'default()')                           NOT NULL,
    [FECREGISTRO]  DATETIME                                                                       NOT NULL,
    [USUREGISTRO]  CHAR (20)                                                                      NOT NULL,
    [FR]           INT                                                                            NULL,
    [SO2]          INT                                                                            NULL,
    CONSTRAINT [PK_HCORHEMSV] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMSV_HCORHEMBOL] FOREIGN KEY ([HCORHEMBOLID]) REFERENCES [dbo].[HCORHEMBOL] ([ID])
);


GO
ALTER TABLE [dbo].[HCORHEMSV] NOCHECK CONSTRAINT [FK_HCORHEMSV_HCORHEMBOL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'USUREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Feha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'T';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tensión Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'TA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Registro: 1-> Al iniciar, 2-> Monitoreo, 3-> Al Finalizar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de bolsa de sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'ID';

