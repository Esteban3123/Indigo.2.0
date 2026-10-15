CREATE TABLE [dbo].[SEGpermir] (
    [codigorol] CHAR (4)    NOT NULL,
    [indidmenu] VARCHAR (5) NOT NULL,
    [ioptguard] BIT         NOT NULL,
    [ioptconsu] BIT         NOT NULL,
    [ioptdisen] BIT         NOT NULL,
    [ioptactua] BIT         NOT NULL,
    [ioptelimi] BIT         NOT NULL,
    [ioptnaveg] BIT         NOT NULL,
    [ioptconfi] BIT         NOT NULL,
    [ioptanula] BIT         NOT NULL,
    [ioptimpri] BIT         NOT NULL,
    [igricrear] BIT         NOT NULL,
    [igrimodif] BIT         NOT NULL,
    [igrielimi] BIT         NOT NULL,
    [ioptvisib] BIT         NOT NULL,
    CONSTRAINT [PK_SEGpermir] PRIMARY KEY CLUSTERED ([codigorol] ASC, [indidmenu] ASC),
    CONSTRAINT [FK_SEGpermir_SEGrolesu] FOREIGN KEY ([codigorol]) REFERENCES [dbo].[SEGrolesu] ([codigorol])
);


GO
ALTER TABLE [dbo].[SEGpermir] NOCHECK CONSTRAINT [FK_SEGpermir_SEGrolesu];


GO


CREATE TRIGGER trg_BlockOperations
ON SEGpermir 
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    RAISERROR('Operación no permitida, contacta al equipo desarollo EHR Cra 5 A No. 22 - 31, Neiva, Huila (Segundo piso)', 16, 1);
    ROLLBACK TRANSACTION;
END;
GO
DISABLE TRIGGER [dbo].[trg_BlockOperations]
    ON [dbo].[SEGpermir];

