'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Jhon Tovar
' Created          : 10-03-2022
'
' Last Modified By :
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.Interface
Imports System.Configuration

#End Region
Public Class ProductCatalogRepository
    Implements IProductCatalogRepository

    Public Function SaveProductCatalog(product As ProductCatalog, companyCode As String) As Boolean Implements IProductCatalogRepository.SaveProductCatalog
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "INSERT INTO Security.ProductCatalog(Id,PlatformName,SuiteName,ProductName,State,Visible) VALUES(@IdProduct,@PlatformName,@SuiteName,@ProductName,@State,@Visible)"

            conx.AddParam("IdProduct", SqlDbType.Int, product.IdProduct)
            conx.AddParam("PlatformName", SqlDbType.VarChar, product.PlatformName)
            conx.AddParam("SuiteName", SqlDbType.VarChar, product.SuiteName)
            conx.AddParam("ProductName", SqlDbType.VarChar, product.ProductName)
            conx.AddParam("State", SqlDbType.Bit, 1)
            conx.AddParam("Visible", SqlDbType.Bit, product.Visible)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function UpdateProductCatalog(product As ProductCatalog, companyCode As String) As Boolean Implements IProductCatalogRepository.UpdateProductCatalog
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.ProductCatalog SET PlatformName = @PlatformName,SuiteName= @SuiteName,ProductName = @ProductName,Visible = @Visible WHERE Id = @IdProduct"

            conx.AddParam("IdProduct", SqlDbType.Int, product.IdProduct)
            conx.AddParam("PlatformName", SqlDbType.VarChar, product.PlatformName)
            conx.AddParam("SuiteName", SqlDbType.VarChar, product.SuiteName)
            conx.AddParam("ProductName", SqlDbType.VarChar, product.ProductName)
            conx.AddParam("Visible", SqlDbType.Bit, product.Visible)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function GetProductCatalog(IdProductCatalog As Integer, companyCode As String) As ProductCatalog Implements IProductCatalogRepository.GetProductCatalog
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim res As ProductCatalog = New ProductCatalog()
            Dim query = String.Format("SELECT Id,PlatformName,SuiteName,ProductName,State,Visible FROM Security.ProductCatalog WHERE Id = {0}", IdProductCatalog)
            Dim dt As DataTable = conx.ExecuteCommand_Data(query)

            If dt.Rows.Count > 0 Then
                res.IdProduct = CInt(dt.Rows(0)("Id"))
                res.PlatformName = dt.Rows(0)("PlatformName").ToString()
                res.SuiteName = dt.Rows(0)("SuiteName").ToString()
                res.ProductName = dt.Rows(0)("ProductName").ToString()
                res.State = Convert.ToByte(dt.Rows(0)("State"))
                res.Visible = Convert.ToByte(dt.Rows(0)("Visible"))
            End If

            Return res
        End Using
    End Function

    Public Function DeleteProductCatalog(IdProductCatalog As Integer, companyCode As String) As Boolean Implements IProductCatalogRepository.DeleteProductCatalog
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "DELETE FROM Security.ProductCatalog WHERE Id = @IdProduct"
            conx.AddParam("IdProduct", SqlDbType.Int, IdProductCatalog)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function ChangeStateProductCatalog(IdProductCatalog As Integer, state As Byte, companyCode As String) As Boolean Implements IProductCatalogRepository.ChangeStateProductCatalog
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.ProductCatalog SET State = @state WHERE Id = @IdProduct"
            conx.AddParam("IdProduct", SqlDbType.Int, IdProductCatalog)
            conx.AddParam("state", SqlDbType.Bit, state)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    Public Function ListProductCatalog() As List(Of ProductCatalog) Implements IProductCatalogRepository.ListProductCatalog
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim list As List(Of ProductCatalog) = New List(Of ProductCatalog)
            Dim query = String.Format("SELECT Id,PlatformName,SuiteName,ProductName,State,Visible FROM Security.ProductCatalog")
            Dim dt As DataTable = conx.ExecuteCommand_Data(query)
            Dim res As ProductCatalog = Nothing

            For index As Integer = 0 To dt.Rows.Count() - 1
                res = New ProductCatalog() With {
                .IdProduct = CInt(dt.Rows(index)("Id")),
                .PlatformName = dt.Rows(index)("PlatformName").ToString(),
                .SuiteName = dt.Rows(index)("SuiteName").ToString(),
                .ProductName = dt.Rows(index)("ProductName").ToString(),
                .State = Convert.ToByte(dt.Rows(index)("State")),
                .Visible = Convert.ToByte(dt.Rows(index)("Visible"))
            }

                list.Add(res)
            Next

            Return list
        End Using
    End Function
End Class
