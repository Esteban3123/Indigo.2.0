CREATE TABLE [dbo].[INDEPARTA] (
    [depcodigo] CHAR (2)     NOT NULL,
    [nomdepart] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [IDPAIS]    INT          NULL,
    CONSTRAINT [PK_INdepatar] PRIMARY KEY CLUSTERED ([depcodigo] ASC)
);

