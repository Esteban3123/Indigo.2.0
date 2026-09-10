CREATE TABLE [dbo].[RCEMPTRANS] (
    [Codigo]              NCHAR (30)    NOT NULL,
    [Nombre]              VARCHAR (100) NOT NULL,
    [Estado]              BIT           NOT NULL,
    [FechaCreacion]       DATETIME      NOT NULL,
    [UsuarioCreacion]     CHAR (20)     NOT NULL,
    [FechaModificacion]   DATETIME      NULL,
    [UsuarioModificacion] CHAR (20)     NULL,
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_RCEMPTRANS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único de la transacción empresarial (INT IDENTITY, clave primaria). Generado automáticamente para cada registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o login que realizó la última modificación del registro. Tipo CHAR(20), referencia a usuario del sistema o profesional de salud que editó la transacción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario que modificó el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro en formato DATETIME. Null si nunca fue modificado. Útil para auditoría y trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'FechaModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'FechaModificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'FechaModificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o login que creó originalmente el registro en el sistema. Tipo CHAR(20). Referencia al profesional o administrador que ingresó la transacción empresarial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creo el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro en formato DATETIME. Campo obligatorio para auditoría, trazabilidad y análisis de transacciones empresariales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'FechaCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'FechaCreacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'FechaCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo, 0=Inactivo. Campo BIT que indica si la transacción empresarial está vigente o desactivada en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro  1-> Activo  0-> Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'Estado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre de la transacción empresarial. Tipo VARCHAR(100). Ej: transferencia, pago, ajuste, devolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'Nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'Nombre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'Nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador de la transacción empresarial. Tipo NCHAR(30), clave de negocio. Permite búsqueda y referencia de transacciones en reportes y procesos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de transacciones o conceptos de recaudación y cobro (RECEM). Registra los códigos y nombres de cada categoría de transacción utilizada en el módulo de recaudo o facturación, junto con su estado activo/inactivo y datos de auditoría de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCEMPTRANS';
