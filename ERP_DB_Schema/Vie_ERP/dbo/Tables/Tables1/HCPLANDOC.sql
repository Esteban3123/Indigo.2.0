CREATE TABLE [dbo].[HCPLANDOC] (
    [CODCONSEC] VARCHAR (6)   NOT NULL,
    [DESCRIPLA] CHAR (80)     NOT NULL,
    [TIPDOCUME] TINYINT       NOT NULL,
    [CODCENATE] CHAR (10)     NOT NULL,
    [NOMARCHIV] CHAR (80)     NOT NULL,
    [EXTARCHIV] CHAR (10)     NOT NULL,
    [ASUNTO]    VARCHAR (100) NULL,
    [CODSERIPS] CHAR (20)     NULL,
    CONSTRAINT [PK_HCPLANDOC] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_CODSERIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCPLANDOC] NOCHECK CONSTRAINT [FK_CODSERIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que se registra Si el tipo Documento es 12 que es coreo electronico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'ASUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extension del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'EXTARCHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'NOMARCHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento:  0. Consentimiento Informado  1. Notificacion Obligatoria de Diagnosticos  2. Laboratorios  3. Patologias  4. Imágenes Diagnosticas  5. Interconsultas  6. Resultados de Servicios Tomados en Sitio  7. Lectura de Imagenes  8. Insumos / Dispositivos  9. Recomendaciones  10. Enfermeria  11. OtrosProcedimientos  12. Correco Electrónico  13. Complicaciones Epicrisis  14. Pronóstico Epicrisis  15. Recomendaciones Epicrisis  16. Certificados asistenciales  17. RealizoLectura  18. Informe otros procedimientos (dashboard médicos)  19. Terapia  20. Instrumentador Quirurgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'TIPDOCUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'DESCRIPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

