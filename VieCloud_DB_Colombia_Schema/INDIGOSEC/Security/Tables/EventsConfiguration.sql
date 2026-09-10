CREATE TABLE [Security].[EventsConfiguration] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (50)  NOT NULL,
    [ContainerId] INT           NOT NULL,
    [Status]      BIT           NOT NULL,
    [UrlQueue]    VARCHAR (MAX) NOT NULL,
    [Topic]       VARCHAR (255) CONSTRAINT [DF_EventsConfiguration_Topic] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_EventsConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EventsConfiguration_Containers] FOREIGN KEY ([ContainerId]) REFERENCES [Security].[Containers] ([Id]),
    CONSTRAINT [IX_EventsConfiguration] UNIQUE NONCLUSTERED ([Code] ASC, [ContainerId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de eventos', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'EventsConfiguration';

