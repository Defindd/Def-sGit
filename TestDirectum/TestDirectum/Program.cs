using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TestDirectum
{

    public class ValidationRule
    {
        public string type { get; set; }
    }

    public class Option
    {
        public string value { get; set; }
        public string text { get; set; }
        public bool selected { get; set; }
    }

    public class Item
    {
        public string value { get; set; }
        public string label { get; set; }
        public bool @checked { get; set; }
        public string type { get; set; }
        public Attributes attributes { get; set; }
    }

    public class Attributes
    {
        public string message { get; set; }
        public string name { get; set; }
        public string placeholder { get; set; }
        public bool? required { get; set; }
        public string value { get; set; }
        public string label { get; set; }
        public string @class { get; set; }
        public List<ValidationRule> validationRules { get; set; }
        public bool? disabled { get; set; }
        public bool? @checked { get; set; }
        public string text { get; set; }
        public List<Option> options { get; set; }
        public List<Item> items { get; set; }
    }

    public class Form
    {
        public string name { get; set; }
        public string postmessage { get; set; }
        public List<Item> items { get; set; }
    }

    public class Root
    {
        public Form form { get; set; }
    }
    class Program
    {
        public static bool Succed=true;
        public static StringBuilder HTMLCode = new StringBuilder();
        public static void StartHTMLForm(string name)//Добавляет тег form
        {
            HTMLCode.Append("<form name=\"").Append(name);
            TagOutput("form");
        }
        public static void FillerToHTML(Item item)//добавляет филлер
        {
            HTMLCode.Append(item.attributes.message).Append("\n");
        }
        public static void UniversalAppends(Item item)//Добавляет атрибуты в зависимости от тега
        {
            if (item.attributes.label != null && item.type == "text") 
                HTMLCode.Append(" ").Append(item.attributes.label).Append(" ");
            TagInput(item.type);
            if(item.attributes.name!=null)
                HTMLCode.Append("name=\"").Append(item.attributes.name).Append("\" ");
            if (item.attributes.placeholder != null&&(item.type=="text"||item.type=="textarea")) 
                HTMLCode.Append("placeholder=\"")
                    .Append(item.attributes.placeholder).Append("\" ");
            if (item.attributes.required == true) 
                HTMLCode.Append("required ");
            if (item.attributes.validationRules[0].type == "text"&& (item.type == "text" || item.type == "textarea")) 
                HTMLCode.Append("pattern=\"^(?!").Append(item.attributes.placeholder).Append(")\" ");
            else if (item.attributes.validationRules[0].type == "email" && (item.type == "text" || item.type == "textarea"))
                HTMLCode.Append("pattern=\"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$\" ");
            else if (item.attributes.validationRules[0].type == "tel" && (item.type == "text" || item.type == "textarea")) 
                HTMLCode.Append("pattern=\"^((\\+?7|8)[ \\-] ?)?((\\(\\d{3}\\))|(\\d{3}))?([ \\-])?(\\d{3}[\\- ]?\\d{2}[\\- ]?\\d{2})$\" ");
           if(item.attributes.value!=null && (item.type=="text"||item.type=="checkbox"))
                HTMLCode.Append("value=\"").Append(item.attributes.value).Append("\" ");
            if (item.attributes.@class != null)
                HTMLCode.Append("class=\"").Append(item.attributes.@class).Append("\" ");
            if (item.attributes.disabled == true) 
                HTMLCode.Append("disabled");
            
        }
        public static void TextOrTextareaToHTML(Item item)//добавляет тег <input type=text... или <textarea...
        {
            UniversalAppends(item);
            TagOutput(item.type);
        }
        public static void EndHTMLForm(string postMessage)//закрывает тег form
        {
            HTMLCode.Append("</form>\n");
            HTMLCode.Append(postMessage);            
        }
        public static void CheckboxToHTML(Item item)//добавляет тег <input type=checkbox...
        {
            UniversalAppends(item);
            if (item.attributes.@checked == true)
            {
                HTMLCode.Append("checked");
            }
            TagOutput(item.type);
        }
        public static void ButtonToHTML(Item item)//добавляет тег <input type=button...
            {
            TagInput(item.type);
            HTMLCode.Append("class=\"").Append(item.attributes.@class).Append("\" ");
            HTMLCode.Append("value=\"").Append(item.attributes.text).Append("\" ");
            TagOutput(item.type);
        }
        public static void SelectToHTML(Item item)//добавляет тег <select...
        {
            UniversalAppends(item);
            HTMLCode.Append(">\n");
            foreach (var option in item.attributes.options)
            {
                HTMLCode.Append("<option value=\"").Append(option.value).Append("\" ");
                if (option.selected == true)
                    HTMLCode.Append("selected>");
                else HTMLCode.Append(">");
                HTMLCode.Append(option.text).Append("</option>\n");
            }
            TagOutput(item.type);
        }
        public static void RadioToHTML(Item item)//добавляяет тег <input type=radio...
            {
            foreach (var radio in item.attributes.items)
            {
               TagInput(item.type);
                HTMLCode.Append("value=\"").Append(radio.value).Append("\" ");
                if (item.attributes.disabled == true) HTMLCode.Append("disabled ");
                HTMLCode.Append("name=\"").Append(item.attributes.name).Append("\" ");
                HTMLCode.Append("class=\"").Append(item.attributes.@class).Append("\" ");
                if (item.attributes.required == true) HTMLCode.Append("required ");
                if (radio.@checked == true) HTMLCode.Append("checked");
                TagOutput(item.type);
                HTMLCode.Append(radio.label).Append("\n");
            }
        }
        public static void TagInput(string tag)//в зависимости от типа добавляет начало тега
        {
            string tempTag = tag;
            if (tempTag == "text" || tempTag == "checkbox" || tempTag == "radio"||tempTag=="button") tempTag = "input";
            switch (tempTag)
            {
                case "select":HTMLCode.Append("<select ");
                    break;
                case "input":HTMLCode.Append("<input type=\"").Append(tag).Append("\" ");
                    break;
                case "button":HTMLCode.Append("<button ");
                    break;
                case "textarea": HTMLCode.Append("<textarea ");
                    break;
            }
        }
       
        public static void TagOutput(string tag)//В зависимости от типа добавляет конец тега
        {
            string tempTag = tag;
            if (tempTag == "text" || tempTag == "checkbox"||tempTag=="button") tempTag = "tagEndLineBreak";
            switch (tempTag)
            {
                case "select":
                    HTMLCode.Append("</select>\n");
                    break;
                case "tagEndLineBreak": HTMLCode.Append(">\n");
                    break;
                case "textarea":HTMLCode.Append("></textarea>\n");
                    break;
                case "form":HTMLCode.Append("\">\n");
                    break;
                case "radio":HTMLCode.Append(">");
                    break;
            }
        }
        public static void ShowTypeEror(string type)//Если тип не обработан, то выдает ошибку
        {
            Console.WriteLine("Тип {0} не найден. Пожалуйста, проверьте входной json файл", type);
            Succed = false;
        }

        public static void GetTypeOfElement(Item item)//вызывает соответствующие типу функции
        {
            switch (item.type)
            {
                case "filler": FillerToHTML(item);
                    break;
                case "text": TextOrTextareaToHTML(item);
                    break;
                case "textarea": TextOrTextareaToHTML(item);
                    break;
                case "checkbox": CheckboxToHTML(item);
                    break;
                case "button": ButtonToHTML(item);
                    break;
                case "select": SelectToHTML(item);
                    break;
                case "radio":RadioToHTML(item);
                    break;
                default: ShowTypeEror(item.type);
                    break;
            }
        }
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText("test.json",Encoding.Default);//необходимо указать путь к файлу
            var Form = new Root();
            Form = JsonSerializer.Deserialize<Root>(jsonString);
            StartHTMLForm(Form.form.name);
            for (int i = 0; i < Form.form.items.Count; i++)
            {
                GetTypeOfElement(Form.form.items[i]);
            }
            if (Succed)
                EndHTMLForm(Form.form.postmessage);
            else
                EndHTMLForm("В ходе создания формы возникла ошибка. Форма была создана, ошибка была проигнорирована");
            Console.WriteLine(HTMLCode.ToString());
            string startDoc = "<html>\n<head></head>\n<body>";
            string endDoc = "</body>\n</html>";
            using StreamWriter sw = new StreamWriter("index.html", false, Encoding.Default);//формирование .html файла
            sw.WriteLine(startDoc);
            sw.Write(HTMLCode.ToString());
            sw.WriteLine(endDoc);
            Console.ReadKey();
        }
    }
}
