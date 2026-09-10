Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DecreaseMaximumLimitRespository
    Inherits GenericRepository(Of DecreaseMaximumLimit)
    Implements IDecreaseMaximumLimitRepository

    ' contexto del repositorio
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
End Class
