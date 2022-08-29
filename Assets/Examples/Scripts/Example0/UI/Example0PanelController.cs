using Suf.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Examples.Scripts.Example0.UI
{
    public class Example0PanelController: Panel
    {
        private void Start()
        {
            // 按钮区
            transform.Find("ExampleBtns/Example1")?.GetComponent<Button>()?.onClick?.AddListener(OnExample1Click);
            transform.Find("ExampleBtns/Example2")?.GetComponent<Button>()?.onClick?.AddListener(OnExample2Click);
            transform.Find("ExampleBtns/Example3")?.GetComponent<Button>()?.onClick?.AddListener(OnExample3Click);
        }
    
        private void OnExample3Click()
        {
            SceneManager.LoadScene("Example3");
        }

        private void OnExample2Click()
        {
            SceneManager.LoadScene("Example2");
        }

        private void OnExample1Click()
        {
            SceneManager.LoadScene("Example1");
        }
    }
}