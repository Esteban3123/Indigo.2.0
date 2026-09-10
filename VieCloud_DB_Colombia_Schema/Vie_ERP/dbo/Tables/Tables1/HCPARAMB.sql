CREATE TABLE [dbo].[HCPARAMB] (
    [ID]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS]           CHAR (20)       NOT NULL,
    [ANALITO]             VARCHAR (200)   NULL,
    [RESULTADO]           TINYINT         NOT NULL,
    [OBLIGATORIO]         INT             NOT NULL,
    [LONGITUD]            INT             NULL,
    [VALMIN]              NUMERIC (18, 2) NULL,
    [VALMAX]              NUMERIC (18, 2) NULL,
    [PERMITEDECI]         INT             NULL,
    [CLASIFICACION]       VARCHAR (200)   NULL,
    [ESTADO]              INT             NOT NULL,
    [TIPOPARACLI]         INT             NULL,
    [USUARIOCREACION]     CHAR (20)       NULL,
    [FECHACREACION]       DATETIME        NULL,
    [USUARIOMODIFICACION] CHAR (20)       NULL,
    [FECHAMODIFICACION]   DATETIME        NULL,
    CONSTRAINT [PK_HCPARAMB] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARAMB_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARAMB_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARAMB_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de parámetro paraclínico (laboratorio, imagen diagnóstica o patología). Tipo: DATETIME. Auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que realizó la última modificación del parámetro paraclínico. FK a SEGusuaru. Tipo: CHAR(20). Auditoría, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de parámetro paraclínico. Tipo: DATETIME. Auditoría, historial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que creó el parámetro paraclínico en el sistema. FK a SEGusuaru. Tipo: CHAR(20). Auditoría, responsabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de parámetro paraclínico: 1=Laboratorio (examen de sangre, orina, etc.), 2=Imagen diagnóstica (radiología, ecografía, TAC), 3=Patologías (histopatología). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'TIPOPARACLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo paraclinico  1->Laboratorio  2->Imagenes Dx  3->Patologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'TIPOPARACLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'TIPOPARACLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del parámetro paraclínico: 1=Activo (disponible en atención), 0=Inactivo (descontinuado, archivado). Tipo: INT. Vigencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro, 1-> activo, 0-> inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría o clasificación del analito dentro del parámetro paraclínico (ej: química sanguínea, hematología, inmunología). Tipo: VARCHAR(200). Semántica diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación del analito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si el campo permite decimales cuando el resultado es de tipo numérico: 1=Sí, 0=No. Tipo: INT. Validación de precisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'PERMITEDECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el campo permite decimales, en caso de ser numérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'PERMITEDECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'PERMITEDECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo permitido para resultados numéricos del analito (rango superior de referencia o validación). Tipo: NUMERIC(18,2). Límite superior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'VALMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Máximo permitido por el campo en caso de que sea numérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'VALMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'VALMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor mínimo permitido para resultados numéricos del analito (rango inferior de referencia o validación). Tipo: NUMERIC(18,2). Límite inferior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'VALMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Minimo permitido por el campo en caso de que sea numérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'VALMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'VALMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitud o tamaño máximo permitido del campo cuando el resultado es tipo texto. Tipo: INT. Restricción de caracteres.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'LONGITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la longitud o tamaño máximo permitido por el campo, solo cuando el resultado sea tipo campo de texto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'LONGITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'LONGITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define si el analito es obligatorio en el parámetro paraclínico: 1=Obligatorio (debe informarse), 2=Opcional. Tipo: INT. Completitud de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si el analito es obligatorio:   1-> si,  2-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato que almacena el resultado del analito: 1=Valor predefinido (lista HCVALPARACL), 2=Campo de texto libre, 3=Campo numérico. Tipo: TINYINT. Estructura del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado que puede almacenar dico analito  1-> Valor Predefinido  2-> Campo Texto  3-> Campo Numérico  cuando se selecciona valor predefinido se deben cargar los datos de la tabla HCVALPARACL y almacenar dicha relación en otra tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del analito o parámetro medido en el estudio paraclínico (ej: glucosa, hemoglobina, creatinina, densidad ósea). Tipo: VARCHAR(200). Término SNOMED/LOINC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'ANALITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analito que contiene el paraclínico en su resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'ANALITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'ANALITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS (Institución Prestadora de Salud) según nomenclatura de servicios RIPS. FK a INCUPSIPS. Tipo: CHAR(20). Identificación del servicio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del parámetro paraclínico. Tipo: INT IDENTITY. Clave de referencia interna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros clínicos (analitos) asociados a servicios o exámenes de laboratorio/paraclínicos. Define qué analitos se capturan por servicio (CUPS), si son obligatorios, su tipo de resultado, rangos de referencia y clasificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMB';
