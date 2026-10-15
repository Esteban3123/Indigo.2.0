CREATE TABLE [dbo].[SEGmenusu] (
    [indidmenu] VARCHAR (5) NOT NULL,
    [indmodulo] CHAR (2)    NOT NULL,
    [indopcion] INT         NOT NULL,
    CONSTRAINT [PK_SEGmenusu] PRIMARY KEY CLUSTERED ([indidmenu] ASC, [indmodulo] ASC),
    CONSTRAINT [FK_SEGmenusu_SEGmodulu] FOREIGN KEY ([indmodulo]) REFERENCES [dbo].[SEGmodulu] ([indmodulo])
);


GO
ALTER TABLE [dbo].[SEGmenusu] NOCHECK CONSTRAINT [FK_SEGmenusu_SEGmodulu];

