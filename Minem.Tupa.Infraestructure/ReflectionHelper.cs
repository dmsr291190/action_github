using Minem.Tupa.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Infraestructure
{
    public class ReflectionHelper
    {
        private static readonly ConcurrentDictionary<string, Func<object, object>> _cache = new();

        public static object GetNestedPropertyValueFast(object obj, string propertyPath)
        {
            if (obj == null || string.IsNullOrEmpty(propertyPath))
                return null;

            // Si ya está en caché, usarla
            if (_cache.TryGetValue(propertyPath, out var func))
                return func(obj);

            // Construcción de la expresión
            ParameterExpression param = Expression.Parameter(typeof(object), "x");
            Expression body = Expression.Convert(param, obj.GetType());

            foreach (var part in propertyPath.Split('.'))
            {
                var prop = body.Type.GetProperty(part);
                if (prop == null) return null;
                body = Expression.Property(body, prop);
            }

            // Compilar la expresión
            func = Expression.Lambda<Func<object, object>>(Expression.Convert(body, typeof(object)), param).Compile();

            // Guardar en caché
            _cache[propertyPath] = func;

            return func(obj);
        }

        public static bool IsList(object obj)
        {
            if (obj == null) return false;

            Type type = obj.GetType();
            return typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
        }

        public static bool IsEntity(object obj)
        {
            return obj != null && !IsList(obj);
        }

        public static string GetValueType(object obj)
        {
            if (obj == null) return Constante.TipoDatoReflection.NULO;

            Type type = obj.GetType();

            if (typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string))
                return Constante.TipoDatoReflection.LISTA;

            if (type == typeof(int) || type == typeof(long) || type == typeof(short) || type == typeof(byte))
                return Constante.TipoDatoReflection.ENTERO;

            if (type == typeof(string) || type == typeof(char))
                return Constante.TipoDatoReflection.CADENA;

            return Constante.TipoDatoReflection.ENTIDAD;
        }
    }
}
