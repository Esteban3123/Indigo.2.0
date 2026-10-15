CREATE TABLE [dbo].[HCDIAGENF] (
    [CODDIAGENF]  VARCHAR (5)   NOT NULL,
    [NOMBDIAGENF] VARCHAR (150) NOT NULL,
    [ESTADO]      INT           NOT NULL,
    CONSTRAINT [PK_HCDIAGENF] PRIMARY KEY CLUSTERED ([CODDIAGENF] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'True = Activo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre diagnostico Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'NOMBDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Causas Valoracion relacionado con tabla HCDIAGCAUPCE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGENF', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';

