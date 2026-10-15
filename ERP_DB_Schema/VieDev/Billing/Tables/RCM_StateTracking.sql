CREATE TABLE [Billing].[RCM_StateTracking] (
    [TrackingID]    INT            IDENTITY (1, 1) NOT NULL,
    [EntityName]    NVARCHAR (50)  NOT NULL,
    [EntityID]      VARCHAR (20)   NOT NULL,
    [StepID]        INT            NOT NULL,
    [ResponsibleID] INT            NOT NULL,
    [StartTime]     DATETIME2 (7)  NOT NULL,
    [EndTime]       DATETIME2 (7)  NULL,
    [Status]        NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK__RCM_Stat__3C19EDD1FC81C2D8] PRIMARY KEY CLUSTERED ([TrackingID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el estado del registro de seguimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el tiempo final del seguimiento de estado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el tiempo de inicio del seguimiento de estado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'StartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del responsable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'ResponsibleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del paso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'StepID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EntityID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del seguimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_StateTracking', @level2type = N'COLUMN', @level2name = N'TrackingID';

