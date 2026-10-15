CREATE TABLE [dbo].[HCPLANCUIENF] (
    [CODPLANCENF] VARCHAR (3)   NOT NULL,
    [NOMPLANCENF] VARCHAR (100) NOT NULL,
    [ESTADO]      INT           NULL,
    [OBSERVACION] VARCHAR (500) NULL,
    CONSTRAINT [PK_HCPLANCUIENF] PRIMARY KEY CLUSTERED ([CODPLANCENF] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Observación  del plan enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANCUIENF', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Estado (1. Activo, 2. Inactivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANCUIENF', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del plan enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANCUIENF', @level2type = N'COLUMN', @level2name = N'NOMPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANCUIENF', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';

