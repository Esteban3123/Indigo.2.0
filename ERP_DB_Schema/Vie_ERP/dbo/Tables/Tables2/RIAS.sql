CREATE TABLE [dbo].[RIAS] (
    [ID]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPRO]            VARCHAR (4)   NOT NULL,
    [NOMBRE]            VARCHAR (100) NOT NULL,
    [EDADMINIMA]        INT           NOT NULL,
    [EDADMAXIMA]        INT           NOT NULL,
    [TIPOEDAD]          INT           NOT NULL,
    [SEXO]              INT           NOT NULL,
    [ESTADO]            INT           NOT NULL,
    [CODUSUCRE]         CHAR (20)     NOT NULL,
    [FECHACREA]         DATETIME      NOT NULL,
    [CODUSUMOD]         CHAR (20)     NULL,
    [FECHAMOD]          DATETIME      NULL,
    [FORMUCARATE]       VARCHAR (MAX) NULL,
    [MENPACNOINS]       VARCHAR (MAX) NULL,
    [MENPACINASI]       VARCHAR (MAX) NULL,
    [APLICARESTRICCION] INT           NULL,
    CONSTRAINT [PK_RIAS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplicar restricción para demanda inducida en HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'APLICARESTRICCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje para cuando el paciente es inasistente:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'MENPACINASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje para cuando el paciente no está inscrito:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'MENPACNOINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula para la caracterización del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'FORMUCARATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-> Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo:  1 - masculino  2 - femenino  3 - ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tipo edad  0 dias  1 mes  2 años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'TIPOEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'edad maxima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'EDADMAXIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'EDADMINIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del programa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo programa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'CODPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'ID';

