CREATE TABLE [dbo].[AGESPECIA] (
    [CODESPECI] CHAR (2)       NOT NULL,
    [DESESPECI] NVARCHAR (150) NOT NULL,
    [ESTESPECI] BIT            NOT NULL,
    CONSTRAINT [PK_AGESPECIA] PRIMARY KEY CLUSTERED ([CODESPECI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la especialidad: Activo=1, Inactivo=0. Bit que indica si la especialidad médica está habilitada para uso en atenciones, procedimientos y asignaciones de profesionales de la salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'ESTESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Especialidad Activo=1;Inactivo=0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'ESTESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'ESTESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre de la especialidad médica (Medicina General, Cardiología, Pediatría, Cirugía, Laboratorio, Imagenología, etc.). Campo de texto para identificar el tipo de servicio o disciplina clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'DESESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'DESESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'DESESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la especialidad, identificador de 2 caracteres alfanuméricos. Clave primaria para referencia en órdenes, atenciones, profesionales y unidades funcionales del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de especialidades médicas disponibles en el sistema. Permite clasificar y activar o desactivar las especialidades utilizadas en el agendamiento y la atención clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA';
