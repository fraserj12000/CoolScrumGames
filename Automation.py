#This script utilizes Selenium and Python's Unittest to test features on the website such as games, the carousel, login credentials, etc

from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import Select
import unittest
import time

class Test(unittest.TestCase):
    def setUp(self):
        #Requires these options to run without a GUI, since it runs on GitHub actions through Linux.
        self.chrome_options = Options()
        self.options = [
            "--headless",
            "--disable-gpu",
            "--window-size=1920,1200",
            "--ignore-certificate-errors",
            "--disable-extensions",
            "--no-sandbox",
            "--disable-dev-shm-usage"
        ]
        for self.option in self.options:
          self.chrome_options.add_argument(self.option)

        #Launches Chrome to run the tests
        self.driver = webdriver.Chrome(options=self.chrome_options)
        
        self.driver.get("http://localhost:32825")
        
        self.driver.implicitly_wait(0.5)

        #Login steps
        self.driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
        time.sleep(2)
        self.email = self.driver.find_element(By.XPATH, '//*[@id="toEmail"]')
        self.button = self.driver.find_element(By.XPATH, '//*[@id="login-submit"]')
        self.email.send_keys("some_fake_guy@etsu.edu")
        time.sleep(1)
        self.button.click()
        time.sleep(2)
        self.driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
        time.sleep(2)
        self.accesscode = self.driver.find_element(By.XPATH, '//*[@id="accessCode"]')
        self.button = self.driver.find_element(By.XPATH, '//*[@id="login-submit"]')
        self.accesscode.send_keys("testSelenium")
        self.button.click()

    #Tests Navbar to assert that it is working properly
    def test_checkNavbar(self):
        self.navitems = self.driver.find_elements(By.XPATH, "/html/body/header/nav/div/div/ul/li")
        assert self.navitems[0].text is not None

        self.navitems = self.driver.find_elements(By.XPATH, "/html/body/header/nav/div/div/ul/li")
        assert self.navitems[1].text is not None

        self.navitems = self.driver.find_elements(By.XPATH, "/html/body/header/nav/div/div/ul/li")
        assert self.navitems[2].text is not None

        self.navitems = self.driver.find_elements(By.XPATH, "/html/body/header/nav/div/div/ul/li")
        assert self.navitems[3].text is not None

    #Tests Asteroid game to see if it runs
    def test_checkJakesGame(self):
        self.driver.execute_script("window.scrollTo(0, document.body.scrollHeight*0.25);")
        time.sleep(2)
        self.jakesgame = self.driver.find_element(By.XPATH, "/html/body/div/main/div/div[2]/div[1]/h3/button")
        self.jakesgame.click()
        self.driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
        time.sleep(2)
        self.playbutton = self.driver.find_element(By.XPATH, "/html/body/div/main/button")
        self.assertEqual(self.playbutton.text, "Play Game")
        self.playbutton.click()
        self.canvas = self.driver.find_elements(By.XPATH, '//*[@id="gameCanvas"]')
        assert self.canvas[0] is not None

    #Tests Snake game to see if it runs
    def test_checkSnakeGame(self):
        self.driver.execute_script("window.scrollTo(0, document.body.scrollHeight*0.25);")
        time.sleep(2)
        self.snakegame = self.driver.find_element(By.XPATH, "/html/body/div/main/div/div[2]/div[2]/h3/button")
        self.snakegame.click()
        self.driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
        time.sleep(2)
        self.playbutton = self.driver.find_element(By.XPATH, "/html/body/div/main/button")
        self.assertEqual(self.playbutton.text, "Play Game")
        self.playbutton.click()
        self.canvas = self.driver.find_elements(By.XPATH, '//*[@id="gameCanvas"]')
        assert self.canvas[0] is not None

    #Tests login for form restriction
    def test_checklogin(self):
        self.logins = ["egglestonj@etsu", "egglestonj", "egglestonj@", "egglestonj@gmail.com", "egglestonj@outlook.com", "egglestonj-@outlook.com"]
        self.driver.get("http://localhost:32825")
        self.driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
        time.sleep(2)
        self.email = self.driver.find_element(By.XPATH, '//*[@id="toEmail"]')
        self.button = self.driver.find_element(By.XPATH, '//*[@id="login-submit"]')
        self.h4 = self.driver.find_element(By.XPATH, '/html/body/div/main/div[3]/div/section/form/h4')
        for login in self.logins:
            self.email.send_keys(login)
            self.button.click()
            time.sleep(2)
            self.email.clear()
            self.assertEqual(self.h4.text, "Enter an ETSU Email")
        self.email.send_keys("some_fake_guy@etsu.edu")
        self.button.click()
        time.sleep(2)
        self.h4 = self.driver.find_element(By.XPATH, '/html/body/div[2]/div/section/form/h4')
        self.assertEqual(self.h4.text, "Enter an Access Code")


    #Tears the testing environment down
    def tearDown(self):
        self.driver.quit()

if __name__ == '__main__':
    unittest.main()
