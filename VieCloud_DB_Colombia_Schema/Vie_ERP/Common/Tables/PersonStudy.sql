CREATE TABLE [Common].[PersonStudy] (
    [Id]                             INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]                       INT            NOT NULL,
    [StudyTypeId]                    INT            NOT NULL,
    [IsFormal]                       BIT            NOT NULL,
    [Name]                           VARCHAR (100)  NOT NULL,
    [StudyCenterId]                  INT            NOT NULL,
    [ProfessionalCardNumber]         VARCHAR (20)   NULL,
    [ProfessionalCardExpeditionDate] DATE           NULL,
    [ProfessionalCardOnProcess]      BIT            NULL,
    [StartDate]                      DATE           NOT NULL,
    [EndingDate]                     DATE           NULL,
    [GraduationDate]                 DATE           NULL,
    [Status]                         TINYINT        NULL,
    [IsInternal]                     BIT            NULL,
    [AverageGrade]                   DECIMAL (2, 1) NULL,
    [StudyValue]                     INT            NULL,
    [StudyValueCompanyPercentage]    INT            NULL,
    [Length]                         INT            NOT NULL,
    [TimeUnitId]                     INT            NOT NULL,
    [CityId]                         INT            NOT NULL,
    [State]                          BIT            NOT NULL,
    CONSTRAINT [PK_EmployeeStudy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmployeeStudy_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_EmployeeStudy_StudyCenter] FOREIGN KEY ([StudyCenterId]) REFERENCES [Payroll].[StudyCenter] ([Id]),
    CONSTRAINT [FK_EmployeeStudy_StudyType] FOREIGN KEY ([StudyTypeId]) REFERENCES [Payroll].[StudyType] ([Id]),
    CONSTRAINT [FK_PersonStudy_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_PersonStudy_TimeUnit] FOREIGN KEY ([TimeUnitId]) REFERENCES [Common].[TimeUnit] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (BIT): 1=activo, 0=inactivo. Indica si el formación del empleado está vigente en el sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro, 0 inactivo, 1 activo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ciudad donde se realiza o realizó el estudio (FK → Common.City). Referencia geográfica de la institución educativa.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'CityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad donde se realiza el estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'CityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de tiempo para duración (FK → Common.TimeUnit). Ejemplo: días, meses, años, horas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'TimeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de tiempo (FK)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'TimeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'TimeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración o extensión numérica del estudio en la unidad de tiempo especificada (INT). Ejemplo: 24 meses, 120 horas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Length';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion del estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Length';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Length';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del valor total del estudio asumido/pagado por la empresa (INT, 0-100%). Complemento de participación económica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyValueCompanyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del valor del estudio asumido por la empresa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyValueCompanyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyValueCompanyPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario total del estudio en pesos COP (INT). Costo de inscripción, matrícula o programa completo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Promedio de calificaciones obtenidas en el estudio (DECIMAL 2.1, rango típico 0-5). Desempeño académico final.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'AverageGrade';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Promedio de notas', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'AverageGrade';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'AverageGrade';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la capacitación/estudio es provisto por la empresa (BIT): 1=proporcionado internamente, 0=externo. Educación corporativa vs. terceros.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'IsInternal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estudio proveeido por la empresa 1 - Si 0 - No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'IsInternal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'IsInternal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del progreso del estudio (TINYINT): 1=en curso, 2=interrumpido, 3=terminado, 4=graduado. Etapa actual de la formación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del estudio   1 - Estudia actualmente   2 - Estudio Interrumpido  3 - Estudio Terminado  4 - Graduado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de graduación o egreso del estudio (DATE). Día en que el empleado completó y se graduó formalmente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'GraduationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Graduacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'GraduationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'GraduationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de terminación o conclusión del período de estudio (DATE). Fin del programa educativo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'EndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de terminacion de estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'EndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'EndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio o matriculación en el estudio (DATE). Comienzo oficial de la formación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la tarjeta/cédula profesional está en trámite (BIT): 1=en proceso, 0=no. Estado de expedición de registro profesional.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardOnProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tarjeta profesional en tramite 1 - Si 0 - No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardOnProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardOnProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de expedición de la tarjeta o cédula profesional (DATE). Cuándo fue entregado el documento de habilitación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardExpeditionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de expedicion de la tarjeta profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardExpeditionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardExpeditionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación de la tarjeta/cédula profesional (VARCHAR 20). Identificador único del registro profesional ante colegio o autoridad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la tarjeta profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'ProfessionalCardNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro educativo/docente (FK → Payroll.StudyCenter). Institución, universidad, academia donde estudió.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro docente (FK)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o título del programa/estudio realizado (VARCHAR 100). Ejemplo: Ingeniería de Sistemas, Diplomado en Gestión, Especialización.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del titulo o estudio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de educación (BIT): 1=formal (reglada/acreditada), 0=no formal (cursos, capacitaciones). Clasificación educativa según regulación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'IsFormal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de educacion 1 - Formal 0 - No formal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'IsFormal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'IsFormal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de estudio/formación (FK → Payroll.StudyType). Categoría: pregrado, postgrado, diploma, certificado, etc.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de estudio FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'StudyTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la persona/empleado (FK → Common.Person). Vinculación con registro maestro de personas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de estudio/formación del empleado (INT IDENTITY, PK). Clave primaria autonumérica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del estudio del empleado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estudios académicos y de formación de las personas (empleados, profesionales de la salud). Registra títulos obtenidos, tarjetas profesionales, instituciones educativas y fechas de inicio, fin y graduación de cada estudio formal o informal.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonStudy';
