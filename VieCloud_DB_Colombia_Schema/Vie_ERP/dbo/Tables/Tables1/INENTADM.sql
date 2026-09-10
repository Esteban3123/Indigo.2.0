CREATE TABLE [dbo].[INENTADM] (
    [CODENTADM]     CHAR (9)      NOT NULL,
    [NOMENTADM]     VARCHAR (150) NOT NULL,
    [CODIGONIT]     CHAR (15)     NOT NULL,
    [TIPENTADM]     CHAR (2)      NOT NULL,
    [REGIMEN]       CHAR (1)      NULL,
    [CODSUPERINTEN] VARCHAR (10)  NULL,
    CONSTRAINT [PK_INENTADM] PRIMARY KEY CLUSTERED ([CODENTADM] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de registro asignado por Superintendencia de Salud o autoridad regulatoria competente, VARCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODSUPERINTEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código asignado por superintendencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODSUPERINTEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODSUPERINTEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Régimen de afiliación: C=Contributivo, S=Subsidiado, E=Excepcional, P=Especial; clasificación de población atendida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'REGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contributivo -> C  Subsidiado ->S  Excepcional->E  Especial->P', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'REGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'REGIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad administradora: EPS, EAS, ARS, ARP, MP, ESE, EEPS, IPS, EMP, PPN, PPJ u OTRAS; versión Fox (1-12) o .NET (0-11).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'TIPENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Entidad  Si es version Fox  1=EPS;  2=EAS;  3=ARS;  4:=ARP;  5=MP;  6=ESE;  7=EEPS;  8=IPS;  9=EMP;  10=PPN;  11=PPJ;  12=OTRAS  Si es version .NET  0=EPS;  1=EAS;  2=ARS;  3=ARP;  4=MP;  5=ESE;  6=EEPS;  7=IPS;  8=EMP;  9=PPN;  10=PPJ;  11=OTRAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'TIPENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'TIPENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Identificación Tributaria (NIT) de la entidad administradora, documento fiscal de registro ante DIAN, CHAR(15), PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Nit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social de la entidad administradora de salud, descripción completa para búsqueda de prestador, CHAR(150).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'NOMENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la entidad administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'NOMENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'NOMENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de entidad administradora (EPS, ARS, IPS, ESE, ARP, MP), identificador principal, CHAR(9), PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Entidad Administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM', @level2type = N'COLUMN', @level2name = N'CODENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de entidades administradoras (aseguradoras, EPS, ARS, ARL, compañías de seguros, etc.) que participan en la atención de pacientes. Contiene el código, nombre, NIT y tipo de cada entidad para su uso en contratos, autorizaciones y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTADM';
