CREATE TABLE [dbo].[HCPLANUNIDADESF] (
    [ID]             INT         IDENTITY (1, 1) NOT NULL,
    [IDHCPLANCUIENF] VARCHAR (3) NOT NULL,
    [UFUCODIGO]      CHAR (10)   NOT NULL,
    CONSTRAINT [PK_HCPLANUNIDADESF] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de  plan de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'IDHCPLANCUIENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'ID';

