CREATE TABLE [Security].[Endpoints] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (30)   NOT NULL,
    [IdContainer] INT            NOT NULL,
    [UrlBase]     VARCHAR (1000) NOT NULL,
    CONSTRAINT [PK_Endpoints] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Endpoints_Containers] FOREIGN KEY ([IdContainer]) REFERENCES [Security].[Containers] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'--- Leonardo Rojas --- 27/06/2024 - PBI 18391 - Creación de parámetro oculto para activación o inactivación de V-Twin (partner o gemelo digital). Se inserta Endpoint vinculado a V-Twin necesario para la integración con Azure OpenAI Service---', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Endpoints';

