CREATE TABLE [dbo].[Table_Inventario] (
    [ID]           INT          IDENTITY (1, 1) NOT NULL,
    [CODIGO]       VARCHAR (50) NULL,
    [CANTIDAD]     INT          NULL,
    [PRESENTACION] INT          NULL,
    CONSTRAINT [PK_Table_Inventario] PRIMARY KEY CLUSTERED ([ID] ASC)
);

