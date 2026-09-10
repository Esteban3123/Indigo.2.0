CREATE TABLE [HumanTalent].[GeneralContractParameters] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractClass]            TINYINT         NOT NULL,
    [PreviousNotificationTerm] TINYINT         NULL,
    [PreviousNotification]     INT             NULL,
    [ExpirationAlertTerm]      INT             NULL,
    [ExpirationAlert]          INT             NULL,
    [TestPeriod]               DECIMAL (18, 2) NULL,
    [TestTerm]                 TINYINT         NULL,
    [MaxTestPeriodTerm]        TINYINT         NULL,
    [MaxTestPeriod]            INT             NULL,
    [MaxTermContract]          TINYINT         NULL,
    [MaxTermContractAmount]    INT             NULL,
    [CreationUser]             VARCHAR (50)    NOT NULL,
    [CreationDate]             DATETIME        NOT NULL,
    [ModificationUser]         VARCHAR (50)    NULL,
    [ModificationDate]         DATETIME        NULL,
    CONSTRAINT [PK_GeneralContractParameters] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de parámetros contractuales (DATETIME). Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del parámetro contractual (VARCHAR 50). Trazabilidad de cambios en talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de parámetros contractuales (DATETIME). Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de parámetros contractuales (VARCHAR 50). Trazabilidad de origen en recursos humanos.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad máxima de duración permitida del contrato laboral fijo (INT). Límite temporal en días, meses o años según MaxTermContract. Contratos indefinidos, duración máxima, profesionales de salud.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTermContractAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar el tiempo maximo del contrato cuando la clase es laboral fijo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTermContractAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTermContractAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para duración máxima del contrato: 1=días, 2=mes, 3=años (TINYINT). Clasificador temporal para MaxTermContractAmount.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTermContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar la condicion de tiempo de la duración maxima del contrato = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTermContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTermContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad máxima permitida del período de prueba (INT). Duración expresada en la unidad definida en MaxTestPeriodTerm. Vinculación laboral, prueba.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTestPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la cantidad(tiempo) del maximo de periodo de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTestPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTestPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para período máximo de prueba: 1=días, 2=mes, 3=años (TINYINT). Clasificador temporal para MaxTestPeriod.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTestPeriodTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar la condicion de tiempo del maximo de periodo de prueba = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTestPeriodTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTestPeriodTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificador del término inicial del período de prueba: 1=Término Inicial (TINYINT). Condición inicial de evaluación laboral.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'TestTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el termino del periodo de prueba: 1-Termino Inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'TestTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'TestTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración del período de prueba en contratación de personal (DECIMAL 18,2). Días, meses o años según configuración. Evaluación inicial, vinculación temporal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'TestPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo periodo de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'TestPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'TestPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de tiempo previo para generar alerta de vencimiento de contrato (INT). Notificación anticipada de renovación o finalización.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ExpirationAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la cantidad(tiempo) de la alerta de vencimiento', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ExpirationAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ExpirationAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para alerta de vencimiento: 1=días, 2=mes, 3=años (INT). Clasificador temporal para ExpirationAlert. Recordatorio automático.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ExpirationAlertTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar la condicion de tiempo de la alerta de vencimiento = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ExpirationAlertTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ExpirationAlertTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de tiempo previo para notificar antes del vencimiento del contrato (INT). Plazo de preaviso a profesional de salud o empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'PreviousNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la cantidad (tiempo) de la notificación previa', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'PreviousNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'PreviousNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para notificación previa: 1=días, 2=mes, 3=años (TINYINT). Clasificador temporal para PreviousNotification. Preaviso laboral.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'PreviousNotificationTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar la condicion de tiempo de la notificación previa = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'PreviousNotificationTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'PreviousNotificationTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o clase de contrato: 1=Otros, 2=Aprendizaje, 3=Laboral Fijo, 4=Laboral Indefinido (TINYINT). Clasificación laboral de profesionales, empleados, recursos humanos.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ContractClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clases de contratos 1 - Otros 2 - Aprendizaje 3 - Laboral Fijo 4 - Laboral Indefinido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ContractClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ContractClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY 1,1) de la tabla GeneralContractParameters. Clave primaria, referencia interna de parámetros contractuales.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros generales de configuración para los contratos laborales del módulo de Talento Humano. Define reglas de notificación previa al vencimiento, alertas de expiración, períodos de prueba y duración máxima según la clase de contrato.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters';
