CREATE TABLE [dbo].[HCENTREGATURNOMEDICA] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [IDHCENTREGATURNOC]       INT            NOT NULL,
    [IDHCENTREGATURNOCPACIEN] INT            NOT NULL,
    [CODPRODUC]               CHAR (20)      NOT NULL,
    [INDAPLMED]               VARCHAR (2000) NULL,
    [NUMEFOLIO]               NCHAR (10)     NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNOMED_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCENTREGATURNOMEDICA_HCENTREGATURNOC] FOREIGN KEY ([IDHCENTREGATURNOC]) REFERENCES [dbo].[HCENTREGATURNOC] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOMEDICA_HCENTREGATURNOCPACIEN] FOREIGN KEY ([IDHCENTREGATURNOCPACIEN]) REFERENCES [dbo].[HCENTREGATURNOCPACIEN] ([ID]),
    CONSTRAINT [FK_HCENTREGATURNOMEDICA_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio único del medicamento dispensado en entrega de turno. Identificador alfanumérico (NCHAR 10) para trazabilidad de medicamentos y consultas en farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de aplicación y administración de medicamentos. Instrucciones clínicas detalladas: dosis, frecuencia, vía, contraindicaciones y precauciones para el paciente (hasta 2000 caracteres, texto libre).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Aplicacion de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno único del producto farmacéutico o medicamento en catálogo. Referencia a tabla IHLISTPRO; utilizado para búsqueda de medicamentos, recetas, dispensación y control de inventario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autónumerico (FK) del paciente en el registro de entrega de turno. Enlaza con tabla de pacientes para asociar medicamentos a la cédula/identificación del usuario final.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo IdAutonumerico de la Tabla Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOCPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autónumerico (FK) de la entrega de turno médico. Referencia al turno/atención clínica en el que se prescribió y dispensó el medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo IdAutonumerico de la Entrega Turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'IDHCENTREGATURNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autónumerico único (PK, INT IDENTITY) del registro de medicamento en entrega de turno. Clave primaria de la tabla para auditoría y referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de medicamentos entregados en cada turno médico, vinculando el turno de entrega con el paciente, el producto/medicamento dispensado, las indicaciones de aplicación y el número de folio del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOMEDICA';
