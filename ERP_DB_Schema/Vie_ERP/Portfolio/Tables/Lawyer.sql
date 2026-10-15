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
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Lawyer__Code]
    ON [Portfolio].[Lawyer]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del abogado  0 - Inactivo  1 - Activo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje a pagar por recuperación. Esta opción se habilita si el type es externo(2)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si el abogado es interno o externo  1 - Interno  2 - Externo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del abogado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del abogado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Lawyer', @level2type = N'COLUMN', @level2name = N'Id';

