CREATE TABLE [dbo].[HCFICHA101] (
    [ID]                                       INT           IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION]                      INT           NOT NULL,
    [AOAVFECHAACCIDENTE]                       DATE          NULL,
    [AOAVDIRECCIONACCIDENTE]                   VARCHAR (100) NULL,
    [AOAVACTIVIDADALMOMENTODELACCIDENTE]       INT           NULL,
    [AOAVCUALOTROARMA]                         VARCHAR (50)  NULL,
    [AOAVTIPOATENCIONINICIAL]                  INT           NULL,
    [AOAVCUALOTROAI]                           VARCHAR (50)  NULL,
    [AOAVLOCALIZACIONAGRESION]                 INT           NULL,
    [AOAVEVIDENCIAHUELLAAGRESION]              BIT           NULL,
    [AOAVPERSONAVIOALANIMALAGRESOR]            BIT           NULL,
    [AOAVANIMALCAPTURADO]                      BIT           NULL,
    [AOAVAGENTEAGRESOR]                        INT           NULL,
    [AOAVMANIFESTACIONESLOCALES]               INT           NULL,
    [AOAVMANIFESTACIONESSISTEMICAS]            INT           NULL,
    [AOAVCOMPLICACIONESLOCALES]                INT           NULL,
    [AOAVCOMPLICACIONESSISTEMICAS]             INT           NULL,
    [AOAVGRAVEDADACCIDENTE]                    INT           NULL,
    [AOAVEMPLEOSUERO]                          BIT           NULL,
    [AOAVTIPOSUERO]                            INT           NULL,
    [AOAVREGISTROINVIMA]                       VARCHAR (50)  NULL,
    [AOAVTIEMPODELACCIDENTEEINICIOTRATAMIENTO] TIME (7)      NULL,
    [AOAVREACCIONESAPLICACIONSUERO]            INT           NULL,
    [AOAVDOSIS]                                INT           NULL,
    [AOAVTIEMPOADMINISTRACIONANTIVENENO]       TIME (7)      NULL,
    [AOAVREMITIDOAOTRAINSTITUCION]             BIT           NULL,
    [VERSION]                                  VARCHAR (20)  NULL,
    [JSON]                                     VARCHAR (MAX) NULL,
    [CODDIAGNO]                                CHAR (4)      NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA101_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica de notificación de accidentes ofídicos y por animales venenosos (AOAV). Registra los datos del evento, la agresión, las manifestaciones clínicas, el tratamiento con suero antiveneno y el diagnóstico, para el reporte epidemiológico al SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de la ficha AOAV.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la ficha general de notificación epidemiológica a la que pertenece este registro de accidente ofídico o por animal venenoso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que ocurrió el accidente o agresión por animal venenoso (mordedura de serpiente, picadura de escorpión, araña, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVFECHAACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVFECHAACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección o lugar donde ocurrió el accidente por animal venenoso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVDIRECCIONACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVDIRECCIONACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad que realizaba el paciente en el momento del accidente (trabajo agrícola, doméstica, recreativa, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVACTIVIDADALMOMENTODELACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVACTIVIDADALMOMENTODELACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otro tipo de mecanismo o arma cuando el campo actividad registra la opción ''''otro''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVCUALOTROARMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVCUALOTROARMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de atención inicial recibida por el paciente tras el accidente (primeros auxilios, atención prehospitalaria, consulta, urgencias, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVTIPOATENCIONINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVTIPOATENCIONINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de atención inicial cuando se seleccionó la opción ''''otro''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVCUALOTROAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVCUALOTROAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica del cuerpo donde ocurrió la agresión o mordedura (pie, mano, pierna, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVLOCALIZACIONAGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVLOCALIZACIONAGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se evidenciaron huellas o marcas de la agresión en el paciente (sí/no).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVEVIDENCIAHUELLAAGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVEVIDENCIAHUELLAAGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el paciente u otra persona vio al animal agresor en el momento del accidente (sí/no).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVPERSONAVIOALANIMALAGRESOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVPERSONAVIOALANIMALAGRESOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el animal agresor fue capturado para identificación (sí/no).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVANIMALCAPTURADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVANIMALCAPTURADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de animal o agente agresor causante del accidente (serpiente, escorpión, araña, raya, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVAGENTEAGRESOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVAGENTEAGRESOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestaciones clínicas locales presentadas en el sitio de la mordedura o picadura (edema, dolor, necrosis, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVMANIFESTACIONESLOCALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVMANIFESTACIONESLOCALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestaciones clínicas sistémicas presentadas por el paciente (sangrado, hipotensión, neurotoxicidad, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVMANIFESTACIONESSISTEMICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVMANIFESTACIONESSISTEMICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicaciones locales derivadas del accidente ofídico o por animal venenoso (infección, amputación, necrosis extensa, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVCOMPLICACIONESLOCALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVCOMPLICACIONESLOCALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicaciones sistémicas derivadas del accidente (falla renal, coagulación intravascular, insuficiencia respiratoria, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVCOMPLICACIONESSISTEMICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVCOMPLICACIONESSISTEMICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de la gravedad del accidente (leve, moderado, grave). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVGRAVEDADACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVGRAVEDADACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se empleó suero antiveneno como parte del tratamiento (sí/no).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVEMPLEOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVEMPLEOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de suero antiveneno administrado al paciente (polivalente, monovalente, antiescorpiónico, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVTIPOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVTIPOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de registro INVIMA del suero antiveneno utilizado en el tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVREGISTROINVIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVREGISTROINVIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo transcurrido entre el momento del accidente y el inicio del tratamiento con antiveneno (horas y minutos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVTIEMPODELACCIDENTEEINICIOTRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVTIEMPODELACCIDENTEEINICIOTRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de reacciones adversas presentadas durante la aplicación del suero antiveneno (anafilaxia, urticaria, ninguna, etc.). Código de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVREACCIONESAPLICACIONSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVREACCIONESAPLICACIONSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis de suero antiveneno administradas al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo total de administración del antiveneno (duración de la infusión o aplicación).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVTIEMPOADMINISTRACIONANTIVENENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVTIEMPOADMINISTRACIONANTIVENENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el paciente fue remitido a otra institución de salud para su manejo o seguimiento (sí/no).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVREMITIDOAOTRAINSTITUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'AOAVREMITIDOAOTRAINSTITUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del formulario o estructura de la ficha AOAV utilizada para el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos completos de la ficha en formato JSON, usados para integración o trazabilidad del formulario electrónico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 asociado al tipo de accidente ofídico o por animal venenoso notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA101', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
