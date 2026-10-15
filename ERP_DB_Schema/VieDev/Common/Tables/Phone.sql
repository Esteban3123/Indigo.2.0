CREATE TABLE [Common].[Phone] (
    [Id]           INT                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPerson]     INT                                                                     NOT NULL,
    [Phone]        VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)') NOT NULL,
    [State]        BIT                                                                     NOT NULL,
    [Synchronized] CHAR (1)                                                                NOT NULL,
    [IdPhoneType]  SMALLINT                                                                NOT NULL,
    CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Phones_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_Phones_PhoneType] FOREIGN KEY ([IdPhoneType]) REFERENCES [Common].[PhoneType] ([Id])
);


GO
ALTER TABLE [Common].[Phone] NOCHECK CONSTRAINT [FK_Phones_Person];


GO
ALTER TABLE [Common].[Phone] NOCHECK CONSTRAINT [FK_Phones_PhoneType];


GO
CREATE NONCLUSTERED INDEX [IX_Phone_IdPerson_IdPhoneType]
    ON [Common].[Phone]([IdPerson] ASC, [IdPhoneType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de telefono  1-Movil  2-Fijo  3-Fax  4-Oficina  5-PIN', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Phone', @level2type = N'COLUMN', @level2name = N'IdPhoneType';

