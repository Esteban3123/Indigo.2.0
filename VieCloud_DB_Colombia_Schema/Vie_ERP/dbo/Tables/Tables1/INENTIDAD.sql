CREATE TABLE [dbo].[INENTIDAD] (
    [CODENTIDA] CHAR (9)     NOT NULL,
    [NOMENTIDA] CHAR (250)   NULL,
    [CODIGONIT] CHAR (15)    NOT NULL,
    [CODADMPAG] CHAR (9)     NULL,
    [DIGITOVER] CHAR (1)     NOT NULL,
    [ENTDIRECC] CHAR (150)   NOT NULL,
    [ENTITELEF] CHAR (15)    NOT NULL,
    [ENTCONTAC] CHAR (250)   NULL,
    [ENTIEMAIL] CHAR (50)    NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INEntidad] PRIMARY KEY CLUSTERED ([CODENTIDA] ASC)
);




GO
CREATE NONCLUSTERED INDEX [_dta_index_INENTIDAD_6_1979922175__K1_2]
    ON [dbo].[INENTIDAD]([CODENTIDA] ASC)
    INCLUDE([NOMENTIDA]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INENTIDAD_6_1979922175__K1_K2]
    ON [dbo].[INENTIDAD]([CODENTIDA] ASC, [NOMENTIDA] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INENTIDAD_6_1979922175__K2_1]
    ON [dbo].[INENTIDAD]([NOMENTIDA] ASC)
    INCLUDE([CODENTIDA]);


GO
ALTER INDEX [_dta_index_INENTIDAD_6_1979922175__K2_1]
    ON [dbo].[INENTIDAD] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_INENTIDAD_CODIGONIT]
    ON [dbo].[INENTIDAD]([CODIGONIT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador/timestamp de auditoría (creación, última modificación). NUMERIC(18), campo técnico para trazabilidad y cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico de la entidad. CHAR(50), contacto digital PII para comunicaciones y notificaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTIEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'E-Mail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTIEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTIEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la persona de contacto o representante de la entidad. CHAR(250), identificación del responsable administrativo o comercial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico de contacto de la entidad. CHAR(15), medio de comunicación directo con la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTITELEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTITELEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTITELEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección física de la entidad (domicilio, sede). CHAR(150), ubicación para correspondencia, notificaciones y localización geográfica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTDIRECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTDIRECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTDIRECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dígito de verificación del NIT. CHAR(1), validador de integridad del número tributario, obligatorio para transacciones reguladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'DIGITOVER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Digito de Verificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'DIGITOVER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'DIGITOVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad administradora pagadora (EPS, aseguradora). CHAR(9), referencia FK a entidad responsable del pago de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODADMPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Administradora Pagador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODADMPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODADMPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Identificación Tributaria (NIT) de la entidad. CHAR(15), código fiscal e identificación única ante autoridades, usado en RIPS y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre legal o razón social de la entidad (aseguradora, prestador, administradora). CHAR(250), información descriptiva para consultas y reportes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'NOMENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'NOMENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'NOMENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la entidad (aseguradora, EPS, IPS, centro de atención). Identificador primario CHAR(9), clave para búsquedas de instituciones de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Directorio maestro de entidades externas (aseguradoras, EPS, ARS, pagadores, empresas contratantes u otras organizaciones) con las que opera la institución. Contiene datos de identificación, contacto y configuración administrativa de cada entidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD';
