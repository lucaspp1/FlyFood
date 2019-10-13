using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FlyFood.DAO.Service
{
    public class FileHelper<T>
    {
        private const string PATH = @"Files\\Data\\";
        private List<string> primarysKey = new List<string>() { "id", "codigo", "cod" };

        public bool Delete(List<T> listGenericObject)
        {
            List<T> GenericObjects = select();
            GenericObjects.RemoveAll(o => listGenericObject.FindAll(ox => GenericEquals(ox, o)).Count > 0);
            return Insert(GenericObjects, out string result);
        }

        public bool Delete(int id)
        {
            List<T> GenericObjects = select();
            GenericObjects.RemoveAll(o => GenericVerifyId(o, id));
            return Insert(GenericObjects, out string result);
        }

        // necessário passar o objeto e o id dele
        public bool Update(T genericObject, int primaryKey)
        {
            Delete(primaryKey);
            return Insert(genericObject, out _);
        }

        public List<T> select()
        {
            List<T> objects = new List<T>();
            // deserialize JSON directly from a file
            foreach (string line in File.ReadAllLines(getPath(typeof(T).Name)))
            {
                T genericObject = JsonConvert.DeserializeObject<T>(line);
                objects.Add(genericObject);
            }
            return objects;
        }

        public bool Insert(T genericObject, out string result)
        {
            try
            {
                T newGenericObject = InsertIdInGeneric(genericObject);
                using (StreamWriter streamWriter = File.AppendText(getPath(genericObject.GetType().Name)))
                {
                    string content = JsonConvert.SerializeObject(newGenericObject);
                    streamWriter.WriteLine(content);
                    result = $"{genericObject.GetType().Name} Inserido com sucesso";
                }
                return true;
            }
            catch (Exception ex)
            {
                result = $"Erro ao inserir {genericObject.GetType().Name} \n" + ex.Message;
                return false;
            }
        }
        private bool Insert(List<T> genericObjects, out string result)
        {
            try
            {
                List<string> listContent = new List<string>();
                foreach (T item in genericObjects)
                {
                    T newObject = AlterIdInGeneric(item, listContent.Count + 1);
                    listContent.Add(JsonConvert.SerializeObject(newObject));
                }
                string path = getPath(typeof(T).Name);
                File.WriteAllLines(path, listContent);
                result = $"Lista de {typeof(T).Name} inserido com sucesso";
                return true;
            }
            catch (Exception ex)
            {
                result = $"Erro ao inserir lista de {typeof(T).Name} \n" + ex.Message;
                return false;
            }
        }

        #region Generics Funcitons
        private T AlterIdInGeneric(T genericObject, int id)
        {
            PropertyInfo[] arrayPropertyInfos = genericObject.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in arrayPropertyInfos)
            {
                if (primarysKey.Contains(propertyInfo.Name.ToLower()))
                    propertyInfo.SetValue(genericObject, id);
            }
            return genericObject;
        }

        private T InsertIdInGeneric(T genericObject)
        {
            PropertyInfo[] arrayPropertyInfos = genericObject.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in arrayPropertyInfos)
            {
                if (primarysKey.Contains(propertyInfo.Name.ToLower()))
                    propertyInfo.SetValue(genericObject, select().Count + 1);
            }
            return genericObject;
        }
        private bool GenericEquals(T genericObject, T genericCompare)
        {
            //We will be defining a PropertyInfo Object which contains details about the class property 
            PropertyInfo[] arrayPropertyInfosFirst = genericCompare.GetType().GetProperties();
            PropertyInfo[] arrayPropertyInfosSecond = genericCompare.GetType().GetProperties();
            if (arrayPropertyInfosSecond.Length != arrayPropertyInfosFirst.Length)
                return false;
            bool verify = true;
            for (int i = 0; i < arrayPropertyInfosFirst.Length; i++)
            {
                if (arrayPropertyInfosFirst[i].Name != arrayPropertyInfosSecond[i].Name)
                    verify = false;
                if (arrayPropertyInfosFirst[i].GetValue(genericObject).ToString()
                        .Equals(arrayPropertyInfosSecond[i].GetValue(genericCompare).ToString())
                   )
                {
                    verify = false;
                }
            }
            return verify;
        }

        private bool GenericVerifyId(T genericObject, int Id)
        {
            //We will be defining a PropertyInfo Object which contains details about the class property 
            PropertyInfo[] arrayPropertyInfos = genericObject.GetType().GetProperties();
            foreach (PropertyInfo item in arrayPropertyInfos)
            {
                if (primarysKey.Contains(item.Name.ToLower()))
                {
                    if (item.GetValue(genericObject).ToString() == Id.ToString())
                        return true;
                }
            }
            return false;
        }

        private string getPath(string nameFile)
        {
            string strFile = PATH + Path.DirectorySeparatorChar + nameFile + ".json"; // pegar o caminho todo
            try
            {
                if (!Directory.Exists(PATH)) // verificar se o diretório não existe
                    Directory.CreateDirectory(PATH); // Criar diretório
                if (!File.Exists(strFile))
                    File.Create(strFile).Close(); // Criar arquivos
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception($"Sem permição para criar arquivo: '{strFile}' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strFile;
        }

        #endregion
    }

}
