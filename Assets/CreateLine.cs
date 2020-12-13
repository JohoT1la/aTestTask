using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLine : MonoBehaviour
{
    LineRenderer line;
    Ray ray;
    Vector3 mousePos; //позиция мыши
    public int currLines = 0; //номер линии
    public List<GameObject> masLine; //лист где будут храниться все созданные линии
    public Material material;
    

    void Start()
    {
        masLine = new List<GameObject>(); // инициализируем лист как лист для GameObject
    }

    void Update()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);


        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject)
                {
                    if (line == null) // проверяем на наличие line на сцене 
                    {
                        Create_Line();
                    }
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //получаем координаты мыши
                    mousePos.z = 0; //координата z делаем 0
                    line.SetPosition(0, mousePos); //начальные координатые
                    line.SetPosition(1, mousePos); //конечные координаты
                    masLine.Add(line.gameObject); //добавляем в массив
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && line)
        {
            if(hit.collider != null)
            {
                if(hit.collider.gameObject)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    line.SetPosition(1, mousePos);
                    line = null; //делаем как обнуление line что бы cоздать новую линию
                    currLines++;
                }
            }
            else
            {
                var lastObject = masLine.Count - 1; //ищем последние значение в массиве

                Destroy(masLine[lastObject]); //удаляем линию со сцены
                masLine.RemoveAt(lastObject); //удаляем элемент из листа
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z)) //делаем проверку на нажатие сочетание  клавиш
        {
            var lastObject = masLine.Count -1;

            Destroy(masLine[lastObject]);
            masLine.RemoveAt(lastObject);
        }
    }

    void Create_Line() //метод создание линии с параметрами
    {
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>(); //создаем line как gameobject и передаем компонент LineRenderer
        line.material = material; // присваеваем материал линии
        line.positionCount = 2; // устонавливаем кол-во вершин
        line.startWidth = 0.15f; // толщина начальной позиции
        line.endWidth = 0.15f; // толщина конечную позиции
        line.useWorldSpace = true; // вкл определение в мировых координатах
    }
}
