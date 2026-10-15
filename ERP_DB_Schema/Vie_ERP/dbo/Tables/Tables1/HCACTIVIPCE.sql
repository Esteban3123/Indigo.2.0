CREATE TABLE [dbo].[HCACTIVIPCE] (
    [CODACTPCE] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMACTPCE] VARCHAR (150) NOT NULL,
    [ESTADO]    INT           NULL,
    [EMINANIOS] INT           NOT NULL,
    [EMINMESES] INT           NOT NULL,
    [EMINDIAS]  INT           NOT NULL,
    [EMAXANIOS] INT           NOT NULL,
    [EMAXMESES] INT           NOT NULL,
    [EMAXDIAS]  INT           NOT NULL,
    [IPSEXO]    CHAR (1)      NOT NULL,
    [TIPOACT]   INT           NULL,
    [APLICAROL] INT           NULL,
    CONSTRAINT [PK_HCACTIVIPCE] PRIMARY KEY CLUSTERED ([CODACTPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica al rol 1->Enfermeria 2->Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'APLICAROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de actividad 1->Continua 2->Unica vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'TIPOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo del paciente Hombre (H), Mujer (M), Intersexual (I) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'IPSEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad maxima en dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad maxima en meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXMESES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad maxima en años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXANIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad minima en dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad minima en meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINMESES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad minima en años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINANIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'True = Activo   False = Inactivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'NOMACTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'CODACTPCE';

