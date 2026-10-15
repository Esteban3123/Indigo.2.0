CREATE TABLE [dbo].[HCVACUADI] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]        VARCHAR (3)   NOT NULL,
    [NOMBRE]        VARCHAR (100) NOT NULL,
    [SEXOAPLICA]    INT           NULL,
    [EDADAPLICA_A]  VARCHAR (80)  NULL,
    [EDADDESDE]     INT           NULL,
    [EDADHASTA]     INT           NULL,
    [ESTADO]        INT           NOT NULL,
    [USUCREACION]   CHAR (20)     NOT NULL,
    [FECHACREACION] DATETIME      NOT NULL,
    [USUMODIFICA]   CHAR (20)     NULL,
    [FECHAMODIFICA] DATETIME      NULL,
    CONSTRAINT [PK_HCVACUADI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCVACUADI_HCVACUADI] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCVACUADI] ([ID])
);


GO
ALTER TABLE [dbo].[HCVACUADI] NOCHECK CONSTRAINT [FK_HCVACUADI_HCVACUADI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-> Activo 2-> Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad hasta que aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'EDADHASTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad desde que aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'EDADDESDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'EDADAPLICA_A';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo Aplica 1:Masculino  2:Femenino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'SEXOAPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACUADI', @level2type = N'COLUMN', @level2name = N'ID';

