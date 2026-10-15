CREATE TABLE [Portfolio].[Lawyer] (
    [Id]               INT                                                                       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)                                                              NOT NULL,
    [Name]             VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "Lawyer_Ofuscado", 0)') NOT NULL,
    [ThirdPartyId]     INT                                                                       NOT NULL,
    [Type]             TINYINT                                                                   NOT NULL,
    [Percentage]       DECIMAL (5, 2)                                                            NULL,
    [Status]           BIT                                                                       NOT NULL,
    [CreationUser]     VARCHAR (20)                                                              NOT NULL,
    [CreationDate]     DATETIME                                                                  NOT NULL,
    [ModificationUser] VARCHAR (20)                                                              NULL,
    [ModificationDate] DATETIME                                                                  NULL,
    [TimeStamp]        ROWVERSION                                                                NOT NULL,
    CONSTRAINT [PK_Lawyer__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Lawyer_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[Lawyer] NOCHECK CONSTRAINT [FK_Lawyer_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del abogado  0 - Inactivo  1 - Activo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si el abogado es interno o externo  1 - Interno  2 - Externo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'Type';

