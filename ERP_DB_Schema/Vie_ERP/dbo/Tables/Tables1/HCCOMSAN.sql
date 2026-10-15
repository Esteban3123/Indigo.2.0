CREATE TABLE [dbo].[HCCOMSAN] (
    [ID]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCOMSAM]                   VARCHAR (20)    NOT NULL,
    [DESCOMSAM]                   VARCHAR (200)   NOT NULL,
    [VALIDAHEMOCLA]               INT             NULL,
    [MinimumContentHemocomponent] NUMERIC (18, 1) NULL,
    [MaximumContentHemocomponent] NUMERIC (18, 1) NULL,
    [State]                       BIT             NULL,
    CONSTRAINT [PK__HCCOMSAN__DE084E6E6079D22F] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del componente Sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contenido máximo de la unidad (ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'MaximumContentHemocomponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contenido mínimo de la unidad (ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'MinimumContentHemocomponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Validar Hemoclasificación de bolsa y paciente 1->Si, 2->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'VALIDAHEMOCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Componete Sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'DESCOMSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Componete Sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'CODCOMSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Componente Sanguineo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCOMSAN', @level2type = N'COLUMN', @level2name = N'ID';

