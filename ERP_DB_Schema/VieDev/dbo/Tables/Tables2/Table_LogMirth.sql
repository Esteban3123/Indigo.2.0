CREATE TABLE [dbo].[Table_LogMirth] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Mensaje] VARCHAR (250) NULL,
    [ER7]     VARCHAR (MAX) NULL,
    [Fecha]   DATETIME      CONSTRAINT [DF_Table_LogMirth_Fecha] DEFAULT ([Common].[getdate]()) NULL,
    CONSTRAINT [PK_Table_LogMirth] PRIMARY KEY CLUSTERED ([ID] ASC)
);

