CREATE TABLE [dbo].[ADACTIVID] (
    [codactivi]  CHAR (4)     NOT NULL,
    [desactivi]  CHAR (80)    NOT NULL,
    [INDAUDFOR]  NUMERIC (18) NOT NULL,
    [RIESGOAGRE] BIT          NULL,
    [ESTADO]     INT          CONSTRAINT [DF_ADACTIVID_ESTADO] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ADactivid] PRIMARY KEY CLUSTERED ([codactivi] ASC)
);

