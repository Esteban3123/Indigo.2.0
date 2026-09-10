CREATE TABLE [dbo].[INAUTORIU] (
    [PKTIPOCON] INT          NOT NULL,
    [CODUSUARI] CHAR (20)    NOT NULL,
    [PKVALORCO] CHAR (20)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NULL,
    CONSTRAINT [PK_INAUTORIU] PRIMARY KEY CLUSTERED ([PKTIPOCON] ASC, [CODUSUARI] ASC, [PKVALORCO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador numérico para trazabilidad y registro de cambios en autorizaciones (PII - auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del valor de autorización en tabla de autorizaciones Indigo Crystal.Net, clave foránea a configuración de permisos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'PKVALORCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla de Autorizaciones - Indigo Crystal.Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'PKVALORCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'PKVALORCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario, identificador único del profesional de salud o operador del sistema (PII - identificación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de contexto/control: 1-Centros de Atención, 2-Unidades Funcionales, 3-Plantillas de Edición, 4-Múltiples Ingresos, 5-Entidades, 6-Especialidades Agendamiento, 7-Salas Agendamiento, 8-Dashboard Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'PKTIPOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado:  1: Centros de Atencion  2: Unidades Funcionales  3: Platillas de Edicion  4: Abrir mas de un Ingreso  5: Entidades  6: Agendamiento: Especialidades  7: Agendamiento: Salas  8: Dashboard: Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'PKTIPOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU', @level2type = N'COLUMN', @level2name = N'PKTIPOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las autorizaciones o permisos asignados a usuarios del sistema, vinculando cada usuario con un tipo de control y un valor de configuración específico. Permite gestionar el acceso y las restricciones funcionales por usuario dentro del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUTORIU';
