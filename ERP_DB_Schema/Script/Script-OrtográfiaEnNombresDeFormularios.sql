begin tran t123
begin try
    UPDATE Security.Form
       SET Name = 'Notas Débito / Crédito'
       WHERE Id = 686;

UPDATE Security.Form
       SET Name = 'Notas Débito / Crédito'
       WHERE Id = 731;

UPDATE Security.Form
       SET Name = 'Lineas de Distribución'
       WHERE Id = '584';
COMMIT TRAN t123;
end TRY
begin catch 
  IF @@TRANCOUNT > 0   
       ROLLBACK TRAN t123;
end catch 