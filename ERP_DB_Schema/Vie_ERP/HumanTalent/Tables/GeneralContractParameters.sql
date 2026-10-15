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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para determinar el tiempo maximo del contrato cuando la clase es laboral fijo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTermContractAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para determinar la condicion de tiempo de la duración maxima del contrato = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTermContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la cantidad(tiempo) del maximo de periodo de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTestPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para determinar la condicion de tiempo del maximo de periodo de prueba = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'MaxTestPeriodTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para el termino del periodo de prueba: 1-Termino Inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'TestTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo periodo de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'TestPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la cantidad(tiempo) de la alerta de vencimiento', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ExpirationAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para determinar la condicion de tiempo de la alerta de vencimiento = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ExpirationAlertTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la cantidad (tiempo) de la notificación previa', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'PreviousNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para determinar la condicion de tiempo de la notificación previa = 1-dias,  2-mes, 3-años', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'PreviousNotificationTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clases de contratos 1 - Otros 2 - Aprendizaje 3 - Laboral Fijo 4 - Laboral Indefinido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'ContractClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'GeneralContractParameters', @level2type = N'COLUMN', @level2name = N'Id';

